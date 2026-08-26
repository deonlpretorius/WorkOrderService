using DigitalTwin.WorkOrderProcessor.Interfaces;
using System.Collections.Concurrent;

/// <summary>
/// Namepsace <c>DigitalTwin.WorkOrderProcessor.Services</c> contains the services for the Work Order Processor service.
/// </summary>
namespace DigitalTwin.WorkOrderService.WorkOrderProcessor.Services
{
    /// <summary>
    /// Class <c>QueueService</c> represents the queue service.
    /// <remarks>
    /// Inherits from IQueueService <see cref="IWorkOrderEventQueueService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderEventQueueService : IWorkOrderEventQueueService
    {
        private readonly ConcurrentQueue<Func<CancellationToken, ValueTask>> _workOrderEvents = new();


        // <inheritdoc />
        public Task<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public void EnqueueWorkOrderEvent(Func<CancellationToken, ValueTask> workOrderEvent)
        {
            throw new NotImplementedException();
        }
    }
}
