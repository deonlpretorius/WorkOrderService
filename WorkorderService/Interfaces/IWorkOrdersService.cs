using WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Interfaces</c> contains the contract for the implementation of business logic for the application.
/// </summary>
namespace WorkOrderService.Interfaces
{
    /// <summary>
    /// Interface <c>IWorkOrderService</c> represents the Work Order Service interface.
    /// </summary>
    public interface IWorkOrdersService
    {
        /// <summary>
        /// Property <c>GetAll</c> retrieves all work orders.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work orders.</returns>
        IEnumerable<WorkOrder> GetAll();

        /// <summary>
        /// Property <c>GetAllAsync</c> retrieves all work orders.
        /// </summary>
        /// <returns>A class containing the interface representing the contract for the collection of work orders.</returns>
        Task<IEnumerable<WorkOrder>> GetAllAsync();

        /// <summary>
        /// Property <c>GetById</c> retrieves a work order by the table identifier.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for a work order.</param>
        /// <returns>A class containing the work order data model. Default is Null.</returns>
        WorkOrder? GetById(string workOrderId);

        /// <summary>
        /// Property <c>GetByIdAsync</c> retrieves a work order by the table identifier.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<WorkOrder?> GetByIdAsync(string workOrderId);

        /// <summary>
        /// Property <c>Create</c> creates a work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        WorkOrder Create(WorkOrder workOrder);

        /// <summary>
        /// Property <c>CreateAsync</c> creates a work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<WorkOrder> CreateAsync(WorkOrder workOrder);

        /// <summary>
        /// Property <c>Update</c> updates an work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        bool Update(string workOrderId, WorkOrder updatedWorkOrder);

        /// <summary>
        /// Property <c>UpdateAsync</c> updates an work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<bool> UpdateAsync(string workOrderId, WorkOrder updatedWorkOrder);

        /// <summary>
        /// Property <c>Delete</c> removes an work order.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A boolean containing the work order removal result.</returns>
        bool Delete(string workOrderId);

        /// <summary>
        /// Property <c>DeleteAsync</c> removes an work order.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A boolean containing the work order removal result.</returns>
        Task<bool> DeleteAsync(string workOrderId);
    }
}
