using DigitalTwin.WebAPI.Enums.WorkOrders;
using DigitalTwin.WebAPI.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Interfaces</c> contains the contract for the implementation of business logic for the application.
/// </summary>
namespace DigitalTwin.WebAPI.Interfaces.WorkOrders
{
    /// <summary>
    /// Interface <c>ISitesService</c> represents the contract for the Work Order History (status change) business logic.
    /// </summary>
    public interface IWorkOrderHistoryService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all the work order history records.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        IEnumerable<WorkOrderHistory> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all the work order history records.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        Task<IEnumerable<WorkOrderHistory>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves all work order history records by specific identifier.
        /// </summary>
        /// <param name="workOrderHistoryId">The string containing the globally unique identifier (GUID) for the work order history table.</param>
        /// <returns>A class containing the work order history data model.</returns>
        WorkOrderHistory? GetById(string workOrderHistoryId);

        /// <summary>
        /// Method <c>GetByIdAsync</c> retrieves all work order history records by specific identifier.
        /// </summary>
        /// <param name="workOrderHistoryId">The string containing the globally unique identifier (GUID) for the work order history table.</param>
        /// <returns>A class containing the work order history data model.</returns>
        Task<WorkOrderHistory?> GetByIdAsync(string workOrderHistoryId);


        /// <summary>
        /// Method <c>GetByWorkOrderId</c> retrieves work order history records related to a single work order.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        IEnumerable<WorkOrderHistory> GetByWorkOrderId(string workOrderId);

        /// <summary>
        /// Method <c>GetByWorkOrderIdAsync</c> retrieves work order history records related to a single work order.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        Task<IEnumerable<WorkOrderHistory>> GetByWorkOrderIdAsync(string workOrderId);

        /// <summary>
        /// Method <c>GetByWorkOrderIdAndStatus</c> retrieves work order history records filtered by work order status.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <param name="status">The enum containing the work order status types.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        IEnumerable<WorkOrderHistory> GetByWorkOrderIdAndStatus(string workOrderId, WorkOrderStatusType status);

        /// <summary>
        /// Method <c>GetByWorkOrderIdAndStatus</c> retrieves work order history records filtered by work order status.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <param name="status">The enum containing the work order status types.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        Task<IEnumerable<WorkOrderHistory>> GetByWorkOrderIdAndStatusAsync(string workOrderId, WorkOrderStatusType status);

        /// <summary>
        /// Method <c>GetByStatus</c> retrieves work order history entries by work order status.
        /// </summary>
        /// <param name="status">The enum containing the work order status type.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        IEnumerable<WorkOrderHistory> GetByStatus(WorkOrderStatusType status);

        /// <summary>
        /// Method <c>GetByStatus</c> retrieves work order history entries by work order status.
        /// </summary>
        /// <param name="status">The enum containing the work order status type.</param>
        /// <returns>An interface representing the contract for the collection of work order history data models.</returns>
        Task<IEnumerable<WorkOrderHistory>> GetByStatusAsync(WorkOrderStatusType status);

        /// <summary>
        /// Method <c>Create</c> creates a work order history record entry.
        /// </summary>
        /// <param name="workOrderHistories">The class containing the work order history data model.</param>
        WorkOrderHistory Create(WorkOrderHistory workOrderHistory);

        /// <summary>
        /// Method <c>CreateAsync</c> creates a work order history record entry.
        /// </summary>
        /// <param name="workOrderHistories">The class containing the work order history data model.</param>
        Task<WorkOrderHistory> CreateAsync(WorkOrderHistory workOrderHistory);
    }
}
