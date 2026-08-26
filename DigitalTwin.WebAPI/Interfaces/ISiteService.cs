using DigitalTwin.WorkOrderService.Models;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Interfaces</c> contains the contract for the implementation of CRUD operations for the application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Interfaces
{
    /// <summary>
    /// Interface <c>ISitesService</c> represents the contract for the Site business logic.
    /// </summary>
    public interface ISiteService
    {
        /// <summary>
        /// Method <c>GetAll</c> retrieves all sites.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of site data models.</returns>
        IEnumerable<Site> GetAll();

        /// <summary>
        /// Method <c>GetAllAsync</c> retrieves all sites.
        /// </summary>
        /// <returns>An interface representing the contract for the collection of site data models.</returns>
        Task<IEnumerable<Site>> GetAllAsync();

        /// <summary>
        /// Method <c>GetById</c> retrieves a site by table identifier.
        /// </summary>
        /// 
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <returns>A class containing the site data model.</returns>
        Site? GetById(string siteId);


        /// <summary>
        /// Method <c>GetById</c> retrieves a site by table identifier.
        /// </summary>
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <returns>A class containing the site data model.</returns>
        Task<Site?> GetByIdAsync(string siteId);

        /// <summary>
        /// Method <c>GetBySiteCode</c> retrieves a site by the site code.
        /// </summary>
        /// <param name="siteCode">The string containing the site code.</param>
        /// <returns>A class containing the site data model.</returns>
        Site? GetBySiteCode(string siteCode);

        /// <summary>
        /// Method <c>GetBySiteCode</c> retrieves a site by the site code.
        /// </summary>
        /// <param name="siteCode">The string containing the site code.</param>
        /// <returns>A class containing the site data model.</returns>
        Task<Site?> GetBySiteCodeAsync(string siteCode);

        /// <summary>
        /// Method <c>Create</c> creates a new site entry.
        /// </summary>
        /// <param name="site">The class containing the site data model.</param>
        /// <returns>A class containing the site data model.</returns>
        Site Create(Site site);

        /// <summary>
        /// Method <c>Create</c> creates a new site entry.
        /// </summary>
        /// <param name="site">The class containing the site data model.</param>
        /// <returns>A class containing the site data model.</returns>
        Task<Site> CreateAsync(Site site);

        /// <summary>
        /// Method <c>Update</c> updates a site entry.
        /// </summary>
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <param name="site">The class containing the sites data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        bool Update(string siteId, Site site);

        /// <summary>
        /// Method <c>Update</c> updates a site entry.
        /// </summary>
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <param name="site">The class containing the sites data model.</param>
        /// <returns>A boolean containing the update result.</returns>
        Task<bool> UpdateAsync(string siteId, Site site);

        /// <summary>
        /// Method <c>Delete</c> removes a site.
        /// </summary>
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <returns>A boolean containing the site removal result.</returns>
        bool Delete(string siteId);

        /// <summary>
        /// Method <c>Delete</c> removes a site.
        /// </summary>
        /// <param name="siteId">The string containing the globally unique identifier (GUID) for the sites table.</param>
        /// <returns>A boolean containing the site removal result.</returns>
        Task<bool> DeleteAsync(string siteId);
    }
}
