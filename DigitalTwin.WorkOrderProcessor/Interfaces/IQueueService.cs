using DigitalTwin.WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.WorkOrderProcessor.Interfaces</c> contains the contract for managing background tasks.
/// </summary>
namespace DigitalTwin.WorkOrderProcessor.Interfaces
{
    /// <summary>
    /// Interface <c>IQueueService</c> represents the contract for the Queue Service.
    /// </summary>
    public interface IQueueService<T>
    {
        void Enqueue(T workOrderEvent);
        bool TryDequeue(out T workOrderEvent);
    }
}
