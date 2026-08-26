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
    public class WorkOrderService : BackgroundService
    {
        /// <summary>
        /// Property <c>_serviceProvider</c> represents the service provider.
        /// <value>An interface representing the contract for the service provider.</value>
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Property <c>_logger</c> represents the logger.
        /// <value>An interface representing the contract for the logging service.</value>
        /// </summary>
        private readonly ILogger<WorkOrderService> _logger;

        /// <summary>
        /// Constructor <c>WorkOrderProcessorWorker</c> is used to instantiate the Work Order Processor.
        /// </summary>
        /// <param name="serviceProvider">The interface representing the service provider.</param>
        /// <param name="logger">The interface representing the logger.</param>
        public WorkOrderService(IServiceProvider serviceProvider, ILogger<WorkOrderService> logger)
        {
            _serviceProvider = serviceProvider;
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
                    await ProcessWorkOrderProgressEventsAsync(stoppingToken);
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
        private async Task ProcessWorkOrderProgressEventsAsync(CancellationToken cancellationToken)
        {
            // Create a scope if you need to use Scoped Services (like Entity Framework DbContext).
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WorkOrderProcessorDbContext>();

                // AsAsyncEnumerable streams the rows efficiently without overwhelming memory.
                var workOrderEvents = await dbContext.WorkOrderEvents.ToListAsync();

                // Create a queue to process the work order events.
                Queue<WorkOrderEvent> workOrderEventsQueue = new Queue<WorkOrderEvent>(workOrderEvents);
                
                //


                // Simulate work
                await Task.Delay(1000, cancellationToken);
            }
        }
    }
}
