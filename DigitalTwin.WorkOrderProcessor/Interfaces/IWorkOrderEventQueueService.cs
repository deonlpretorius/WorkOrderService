/// <summary>
/// Namespace <c>DigitalTwin.WorkOrderProcessor.Interfaces</c> contains the contract for managing background tasks.
/// </summary>
namespace DigitalTwin.WorkOrderProcessor.Interfaces
{
    /// <summary>
    /// Interface <c>IQueueService</c> represents the contract for the Queue Service.
    /// </summary>
    public interface IWorkOrderEventQueueService
    {
        /// <summary>
        /// Method <c>EnqueueWorkOrderEvent</c> enqueues a work order event item into the queue.
        /// </summary>
        /// <param name="workOrderEvent">The function specifying a work order event item to enqueue.</param>
        void EnqueueWorkOrderEvent(Func<CancellationToken, ValueTask> workOrderEvent);

        /// <summary>
        /// Method <c>DequeueAsync</c> dequeues a work order event item.
        /// </summary>
        /// <param name="cancellationToken">The struct representing the cancellation token.</param>
        /// <returns>A function representing the return result.</returns>
        Task<Func<CancellationToken, ValueTask>> DequeueAsync(CancellationToken cancellationToken);
    }
}
