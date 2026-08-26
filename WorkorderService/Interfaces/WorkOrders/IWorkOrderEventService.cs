using DigitalTwin.WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Interfaces.WorkOrders</c> contains the contract for the implementation of CRUD operations for the application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders
{
    /// <summary>
    /// Interface <c>IWorkOrderEventService</c> represents the contract for the Work Order Events data access operations.
    /// </summary>
    public interface IWorkOrderEventService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all work order events.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work order event data models.</returns>
        IEnumerable<WorkOrderEvent> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all work order events.
        /// This is the asynchronous function call.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of work order event data models.</returns>
        Task<IEnumerable<WorkOrderEvent>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves a single work order event by identifier.
        /// </summary>
        /// <param name="workOrderEventId">The string containing the globally unique identifier (GUID) for the work order event.</param>
        /// <returns>A class containing the work order event data model.</returns>
        WorkOrderEvent? GetById(string workOrderEventId);

        /// <summary>
        /// Method <c>GetByIdAsync</c> retrieves a single work order event by identifier.
        /// This is the asynchronous function call.
        /// </summary>
        /// <param name="workOrderEventId">The string containing the globally unique identifier (GUID) for the work order event.</param>
        /// <returns>A class containing the work order event data model.</returns>
        Task<WorkOrderEvent?> GetByIdAsync(string workOrderEventId);

        /// <summary>
        /// Method <c>Create</c> is the creation of a new work order event.
        /// </summary>
        /// <param name="workOrderEvent">The class representing the work order events data model.</param>
        /// <returns>A class containing the work order events data model.</returns>
        WorkOrderEvent? Create(WorkOrderEvent workOrderEvent);


        /// <summary>
        /// Method <c>CreateAsync</c> is the creation of a new work order event.
        /// This is the asynchronous function call.
        /// </summary>
        /// <param name="workOrderEvent">The class representing the work order events data model.</param>
        /// <returns>A class containing the work order events data model.</returns>
        Task<WorkOrderEvent?> CreateAsync(WorkOrderEvent workOrderEvent);
    }
}
