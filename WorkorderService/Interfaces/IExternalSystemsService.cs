using WorkOrderService.Models;

/// <summary>
/// Namespace <c>WorkOrderService.Interfaces</c> contains the contract for the implementation of business logic for the application.
/// </summary>
namespace WorkOrderService.Interfaces
{
    /// <summary>
    /// Interface <c>IExternalSystemsService</c> represents the contract for the External System business logic.
    /// </summary>
    public interface IExternalSystemsService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all External Systems.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of External System data models.</returns>
        IEnumerable<ExternalSystem> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all External Systems.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of External System data models.</returns>
        Task<IEnumerable<ExternalSystem>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves a External System by table identifier.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <returns>A class containing the ExternalSystem data model.</returns>
        ExternalSystem? GetById(string externalSystemId);


        /// <summary>
        /// Method <c>GetById</c> retrieves a External System by table identifier.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <returns>A class containing the ExternalSystem data model.</returns>
        Task<ExternalSystem?> GetByIdAsync(string externalSystemId);

        /// <summary>
        /// Method <c>Create</c> creates a new External System entry.
        /// </summary>
        /// <param name="ExternalSystem">The class containing the External System data model.</param>
        /// <returns>A class containing the ExternalSystem data model.</returns>
        ExternalSystem Create(ExternalSystem externalSystem);

        /// <summary>
        /// Method <c>Create</c> creates a new External System entry.
        /// </summary>
        /// <param name="ExternalSystem">The class containing the External System data model.</param>
        /// <returns>A class containing the ExternalSystem data model.</returns>
        Task<ExternalSystem> CreateAsync(ExternalSystem externalSystem);

        /// <summary>
        /// Method <c>Update</c> updates a External System entry.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <param name="ExternalSystem">The class containing the ExternalSystems data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        bool Update(string externalSystemId, ExternalSystem externalSystem);

        /// <summary>
        /// Method <c>Update</c> updates a External System entry.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <param name="ExternalSystem">The class containing the ExternalSystems data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        Task<bool> UpdateAsync(string externalSystemId, ExternalSystem externalSystem);

        /// <summary>
        /// Method <c>Delete</c> removes a External System.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <returns>A boolean containing the ExternalSystem removal result.</returns>
        bool Delete(string externalSystemId);

        /// <summary>
        /// Method <c>Delete</c> removes a External System.
        /// </summary>
        /// <param name="externalSystemId">The string containing the globally unique identifier (GUID) for the External Systems table.</param>
        /// <returns>A boolean containing the ExternalSystem removal result.</returns>
        Task<bool> DeleteAsync(string externalSystemId);
    }
}
