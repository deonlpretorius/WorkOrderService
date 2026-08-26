using DigitalTwin.WorkOrderService.Models;
using DigitalTwin.WorkOrderService.WebAPI.Data;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Services
{
    /// <summary>
    /// Class <c>SitesService</c> represents the Sites Service.
    /// <remarks>
    /// Implements the ISitesService <see cref="ISiteService"/>
    /// </remarks>
    /// </summary>
    public class SiteService : ISiteService
    {
        /// <summary>
        /// Property <c>WorkOrderServiceDbContext</c> represents the database context.
        /// <value>A class containing the data access layer.</value>
        /// </summary>
        private readonly WorkOrderWebServiceWebAPIDbContext _dbContext;

        /// <summary>
        /// Constructor <c>WorkOrderService</c> is used to instantiate the Work Orders Service.
        /// </summary>
        /// <param name="dbContext">The class representing the database context.</param>
        public SiteService(WorkOrderWebServiceWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public Site Create(Site site)
        {
            if (site is null)
                throw new Exception("The site is empty.");

            var newSite = new Site()
            {
                SiteName = site.SiteName,
                SiteDescription = site.SiteDescription,
                SiteCode = site.SiteCode
            };

            _dbContext.Sites.Add(newSite);
            if (_dbContext.SaveChanges() > 1)
                return newSite;

            return site;
        }

        // <inheritdoc />
        public async Task<Site> CreateAsync(Site site)
        {
            if (site is null)
                throw new Exception("The site is empty.");

            var newSite = new Site()
            {
                SiteName = site.SiteName,
                SiteDescription = site.SiteDescription,
                SiteCode = site.SiteCode
            };

            await _dbContext.Sites.AddAsync(newSite);
            if (await _dbContext.SaveChangesAsync() > 1)
                return newSite;

            return site;
        }

        // <inheritdoc />
        public bool Delete(string siteId)
        {
            if (!string.IsNullOrWhiteSpace(siteId))
                return false;

            var site = _dbContext.Sites.Find(siteId);
            if (site is null)
                return false;

            _dbContext.Sites.Remove(site);
            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> DeleteAsync(string siteId)
        {
            if (!string.IsNullOrWhiteSpace(siteId))
                return false;

            var site = await _dbContext.Sites.FindAsync(siteId);
            if (site is null)
                return false;

            _dbContext.Sites.Remove(site);
            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public IEnumerable<Site> GetAll()
        {
            IEnumerable<Site> sites;
            sites = _dbContext.Sites.AsEnumerable();

            if (!sites.Any())
                return null;

            return sites;
        }

        // <inheritdoc />
        public Task<IEnumerable<Site>> GetAllAsync() => Task.FromResult(_dbContext.Sites.AsEnumerable());

        // <inheritdoc />
        public Site? GetById(string siteId)
        {
            var site = _dbContext.Sites.Find(siteId);
            if (site is null)
                return null;

            return site;
        }

        // <inheritdoc />
        public async Task<Site?> GetByIdAsync(string siteId)
        {
            var site = await _dbContext.Sites.FindAsync(siteId);

            if (site is null)
                return null;

            return site;
        }

        // <inheritdoc />
        public Site? GetBySiteCode(string siteCode)
        {
            var site = _dbContext.Sites.Where(x => x.SiteCode == siteCode).FirstOrDefault();
            if (site is null)
                return null;

            return site;
        }

        // <iheritdoc />
        public async Task<Site?> GetBySiteCodeAsync(string siteCode)
        {
            var site = await _dbContext.Sites.Where(x => x.SiteCode == siteCode).FirstOrDefaultAsync();
            if (site is null)
                return null;

            return site;
        }

        // <inheritdoc />
        public bool Update(string siteId, Site site)
        {
            var currentSite = _dbContext.Sites.Find(siteId);
            if (currentSite is null)
                throw new Exception("The existing site to update could not found.");

            currentSite.SiteName = site.SiteName;
            currentSite.SiteDescription = site.SiteDescription;
            currentSite.SiteCode = site.SiteCode;
            currentSite.LastModified = DateTime.Now;

            _dbContext.Sites.Update(currentSite);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;

        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string siteId, Site site)
        {
            var currentSite = await _dbContext.Sites.FindAsync(siteId);
            if (currentSite is null)
                return false;

            currentSite.SiteName = site.SiteName;
            currentSite.SiteDescription = site.SiteDescription;
            currentSite.SiteCode = site.SiteCode;
            currentSite.LastModified = DateTime.Now;

            _dbContext.Sites.Update(currentSite);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;

        }
    }
}
