using DigitalTwin.Enums.WorkOrders;
using DigitalTwin.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Interfaces.WorkOrders</c> contains the contract for the implementation of business logic for the application.
/// </summary>
namespace DigitalTwin.WebAPI.Interfaces.WorkOrders
{
    /// <summary>
    /// Interface <c>IWorkOrderStatussService</c> represents the contract for the Work Order Status business logic.
    /// </summary>
    public interface IWorkOrderStatusService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all Work Order Statuss.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of Work Order Status data models.</returns>
        IEnumerable<WorkOrderStatus> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all Work Order Statuss.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of Work Order Status data models.</returns>
        Task<IEnumerable<WorkOrderStatus>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves a WorkOrderStatus by table identifier.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <returns>A class containing the Work Order Status data model.</returns>
        WorkOrderStatus? GetById(string workOrderStatusId);


        /// <summary>
        /// Method <c>GetById</c> retrieves a Work Order Status by table identifier.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <returns>A class containing the Work Order Status data model.</returns>
        Task<WorkOrderStatus?> GetByIdAsync(string workOrderStatusId);

        /// <summary>
        /// Method <c>GetByStatus</c> retrieves a work order status by the status type.
        /// </summary>
        /// <param name="status">The enum containing the work order status type.</param>
        /// <returns>A class containing the work order status data model.</returns>
        WorkOrderStatus? GetByStatus(WorkOrderStatusType status);

        /// <summary>
        /// Method <c>GetByStatusAsync</c> retrieves a work order status by the status type.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="status">The enum containing the work order status type.</param>
        /// <returns>A class containing the work order status data model.</returns>
        Task<WorkOrderStatus?> GetByStatusAsync(WorkOrderStatusType status);

        /// <summary>
        /// Method <c>Create</c> creates a new Work Order Status entry.
        /// </summary>
        /// <param name="WorkOrderStatus">The class containing the Work Order Status data model.</param>
        /// <returns>A class containing the Work Order Status data model.</returns>
        WorkOrderStatus Create(WorkOrderStatus workOrderStatus);

        /// <summary>
        /// Method <c>Create</c> creates a new Work Order Status entry.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="WorkOrderStatus">The class containing the Work Order Status data model.</param>
        /// <returns>A class containing the Work Order Status data model.</returns>
        Task<WorkOrderStatus> CreateAsync(WorkOrderStatus workOrderStatus);

        /// <summary>
        /// Method <c>Update</c> updates a Work Order Status entry.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <param name="WorkOrderStatus">The class containing the Work Order Statuss data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        bool Update(string workOrderStatusId, WorkOrderStatus workOrderStatus);

        /// <summary>
        /// Method <c>Update</c> updates a Work Order Status entry.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <param name="WorkOrderStatus">The class containing the Work Order Statuss data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        Task<bool> UpdateAsync(string workOrderStatusId, WorkOrderStatus workOrderStatus);

        /// <summary>
        /// Method <c>Delete</c> removes a Work Order Status.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <returns>A boolean containing the Work Order Status removal result.</returns>
        bool Delete(string workOrderStatusId);

        /// <summary>
        /// Method <c>Delete</c> removes a Work Order Status.
        /// This is a asynchronous function call.
        /// </summary>
        /// <param name="workOrderStatusId">The string containing the globally unique identifier (GUID) for the Work Order Statuss table.</param>
        /// <returns>A boolean containing the Work Order Status removal result.</returns>
        Task<bool> DeleteAsync(string workOrderStatusId);
    }
}
