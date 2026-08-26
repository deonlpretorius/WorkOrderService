using DigitalTwin.WorkOrderProcessor.Interfaces;
using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WorkOrderProcessor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

/// <summary>
/// Namespace <c>DigitalTwin.WorkOrderProcessor</c> contains the Work Order Progress Event Processor.
/// </summary>
namespace DigitalTwin.WorkOrderService.WorkOrderProcessor
{
    /// <summary>
    /// Class <c>WorkOrderProcessorWorker</c> represents the Work Order Processor as a background service.
    /// <remarks>
    /// Inherit from BackgroundService <see cref="BackgroundService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderProcessorService : BackgroundService
    {
        /// <summary>
        /// Property <c>_serviceProvider</c> represents the service provider.
        /// <value>An interface representing the contract for the service provider.</value>
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Property <c>_queueService</c> represents the queue service.
        /// <value>An interface representing the contract for the queue service.</value>
        /// </summary>
        private readonly IQueueService<WorkOrderEvent> _queueService;

        /// <summary>
        /// Property <c>_logger</c> represents the logger.
        /// <value>An interface representing the contract for the logging service.</value>
        /// </summary>
        private readonly ILogger<WorkOrderProcessorService> _logger;

        /// <summary>
        /// Constructor <c>WorkOrderProcessorWorker</c> is used to instantiate the Work Order Processor.
        /// </summary>
        /// <param name="serviceProvider">The interface representing the service provider.</param>
        /// <param name="logger">The interface representing the logger.</param>
        public WorkOrderProcessorService(IServiceProvider serviceProvider,
                                ILogger<WorkOrderProcessorService> logger,
                                IQueueService<WorkOrderEvent> queueService)
        {
            _serviceProvider = serviceProvider;
            _queueService = queueService;
            _logger = logger;
        }

        /// <summary>
        /// Method <c>ExecuteAsync</c> is used to execute the background worker.
        /// </summary>
        /// <param name="stoppingToken">The struct representing the cancellation token.</param>
        /// <returns>A class containing the return result.</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Create a scope if you need to use Scoped Services (like Entity Framework DbContext).
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<WorkOrderProcessorDbContext>();

                        await EnqueueWorkOrderEventAsync(dbContext, stoppingToken);

                        await ProcessWorkOrderEventsAsync(dbContext, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while processing the Work Order Events table.");
                }

                // Wait 5 seconds before the next check.
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }

            _logger.LogInformation("Work Order Processor is stopping.");
        }

        /// <summary>
        /// Method <c>ProcessWorkOrderProgressEventsAsync</c> performs the Work Order Event processing.
        /// </summary>
        /// <param name="cancellationToken">The struct representing the cancellation token.</param>
        /// <returns>A class representing the return result.</returns>
        private async Task EnqueueWorkOrderEventAsync(WorkOrderProcessorDbContext dbContext, CancellationToken cancellationToken)
        {
            var workOrderEvents = await dbContext.WorkOrderEvents.AsNoTracking()
                                                                 .ToListAsync();

            // Enqueue the work order events
            foreach (var workOrderEvent in workOrderEvents)
            {
                _queueService.Enqueue(workOrderEvent);
            }
        }

        private async Task ProcessWorkOrderEventsAsync(WorkOrderProcessorDbContext dbContext, CancellationToken stoppingToken)
        {
            if (_queueService.TryDequeue(out var workOrderEvent))
            {
                // Add the logic here to update the respective work orders.
                _logger.LogInformation("Processing record: {workOrderEvent}", workOrderEvent);
                var workOrder = await dbContext.WorkOrders.FindAsync(workOrderEvent.WorkOrderId);
                // If work order does not exist just update the work order details field.
                if (workOrder is  null)
                {
                    workOrderEvent.Details += "The work order for this progress event could not be found.";
                    dbContext.WorkOrderEvents.Update(workOrderEvent);
                    await dbContext.SaveChangesAsync();
                }

                // Update the Work Order Status from the Work Order Event.
                workOrder?.WorkOrderStatus = workOrderEvent.WorkOrderStatus;
                workOrder?.WorkOrderStatusId = workOrderEvent.WorkOrderStatusId;

                // Make a work order status change history entry for the current work order.
                var workOrderHistory = new WorkOrderHistory()
                {
                    WorkOrderStatusId = workOrderEvent.WorkOrderStatusId,
                    WorkOrderStatus = workOrderEvent.WorkOrderStatus,
                    WorkOrderId = workOrder.WorkOrderId,
                    WorkOrder = workOrder
                };
                dbContext.WorkOrderHistories.Update(workOrderHistory);

                // Remove the Work Order Event record from the table.
                dbContext.WorkOrderEvents.Remove(workOrderEvent);

                await dbContext.SaveChangesAsync();

                // Simulate work
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
