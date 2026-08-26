using DigitalTwin.Enums.WorkOrders;
using DigitalTwin.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Interfaces.WorkOrders</c> contains the contract for the implementation of business logic for the application.
/// </summary>
namespace DigitalTwin.WebAPI.Interfaces.WorkOrders
{
    /// <summary>
    /// Interface <c>IWorkOrderService</c> represents the Work Order Service interface.
    /// </summary>
    public interface IWorkOrderService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all work orders.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work orders.</returns>
        IEnumerable<WorkOrder> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all work orders.
        /// This is a synchronous function call.
        /// </summary>
        /// <returns>A class containing the interface representing the contract for the collection of work orders.</returns>
        Task<IEnumerable<WorkOrder>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves a work order by the table identifier.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for a work order.</param>
        /// <returns>A class containing the work order data model. Default is Null.</returns>
        WorkOrder? GetById(string workOrderId);

        /// <summary>
        /// Method <c>GetByIdAsync</c> retrieves a work order by the table identifier.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<WorkOrder?> GetByIdAsync(string workOrderId);

        /// <summary>
        /// Method <c>GetByStatus</c> retrieves all work orders by work order status.
        /// </summary>
        /// <param name="status">The enumeration representing the work order status type.</param>
        /// <returns>An interface representing the contract for the collection of work orders.</returns>
        IEnumerable<WorkOrder> GetByStatus(WorkOrderStatusType status, int pageNumber, int pageSize);

        /// <summary>
        /// Method <c>GetByStatusAsync</c> retrieves all work orders by work order status.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="status">The enumeration representing the work order status type.</param>
        /// <param name="pageNumber">The integer containing the page number.</param>
        /// <param name="pageSize">The integer containing the fixed page size.</param>
        /// <returns></returns>
        Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatusType status, int pageNumber, int pageSize);

        /// <summary>
        /// Method <c>getByExternalId</c> retrieves a work order by the external system identifier.
        /// </summary>
        /// <param name="workOrderExternalId">The string containing the external system identfier for the work order.</param>
        /// <returns>A class containing the work order data model.</returns>
        WorkOrder? GetByExternalId(string workOrderExternalId);

        /// <summary>
        /// Method <c>GetByExternalId</c> retrieves a work order by the external system identifier.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderExternalId">The string containing the external system identfier for the work order.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<WorkOrder?> GetByExternalIdAsync(string workOrderExternalId);

        /// <summary>
        /// Method <c>Create</c> creates a work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        WorkOrder Create(WorkOrder workOrder);

        /// <summary>
        /// Method <c>CreateAsync</c> creates a work order.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<WorkOrder> CreateAsync(WorkOrder workOrder);

        /// <summary>
        /// Method <c>Update</c> updates an work order.
        /// </summary>
        /// <param name="workOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        bool Update(string workOrderId, WorkOrder updatedWorkOrder);

        /// <summary>
        /// Method <c>UpdateAsync</c> updates an work order.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <param name="updatedWorkOrder">The class representing the work order table.</param>
        /// <returns>A class containing the work order data model.</returns>
        Task<bool> UpdateAsync(string workOrderId, WorkOrder updatedWorkOrder);

        /// <summary>
        /// Method <c>UpdateWorkOrderStatus</c> updates a work order status in the work order table.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <param name="status">The enum containing the work order status types.</param>
        /// <returns>A boolean containing the update status result.</returns>
        bool UpdateWorkOrderStatus(string workOrderId, WorkOrderStatusType status);

        /// <summary>
        /// Method <c>UpdateWorkOrderStatusAsync</c> updates a work order status in the work order table.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <param name="status">The enum containing the work order status types.</param>
        /// <returns>A boolean containing the update status result.</returns>
        Task<bool> UpdateWorkOrderStatusAsync(string workOrderId, WorkOrderStatusType status);

        /// <summary>
        /// Method <c>Delete</c> removes an work order.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A boolean containing the work order removal result.</returns>
        bool Delete(string workOrderId);

        /// <summary>
        /// Method <c>DeleteAsync</c> removes an work order.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderId">The string containing the globally unique identifier (GUID) for the work order table.</param>
        /// <returns>A boolean containing the work order removal result.</returns>
        Task<bool> DeleteAsync(string workOrderId);
    }
}
