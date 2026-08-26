using DigitalTwin.WorkOrderProcessor.Interfaces;
using System.Collections.Concurrent;

/// <summary>
/// Namepsace <c>DigitalTwin.WorkOrderService.Services</c> contains the services for the Work Order Processor service.
/// </summary>
namespace DigitalTwin.WorkOrderService.WorkOrderProcessor.Services
{
    /// <summary>
    /// Class <c>WorkOrderEventQueueService</c> represents the queue service.
    /// <remarks>
    /// Inherits from IQueueService <see cref="IQueueService"/>
    /// </remarks>
    /// </summary>
    public class QueueService<T> : IQueueService<T>
    {
        /// <summary>
        /// Property <c>_queue</c> represents the concurrent queue.
        /// <value>A class containing the concurrent queue.</value>
        /// </summary>
        private readonly ConcurrentQueue<T> _queue = new();

        /// <summary>
        /// Method <c>Enqueue</c> enqueues an item on the concurrent queue.
        /// </summary>
        /// <param name="item">The generic item to enqueue.</param>
        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
        }

        /// <summary>
        /// Method <c>TryDequeue</c> dequeues an item on the concurrent queue.
        /// </summary>
        /// <param name="item">The generic item dequeued from the concurrent queue.</param>
        /// <returns>A boolean containing the return result.</returns>
        public bool TryDequeue(out T item)
        {
            return _queue.TryDequeue(out item);
        }
    }
}
