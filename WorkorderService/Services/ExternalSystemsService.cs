using WorkOrderService.Data;
using WorkOrderService.Interfaces;
using WorkOrderService.Models;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Services
{
    /// <summary>
    /// Class <c>SitesService</c> represents the Sites Service.
    /// <remarks>
    /// Implements the ISitesService <see cref="ISitesService"/>
    /// </remarks>
    /// </summary>
    public class ExternalSystemsService : IExternalSystemsService
    {
        /// <summary>
        /// Property <c>WorkOrderServiceDbContext</c> represents the database context.
        /// <value>A class containing the data access layer.</value>
        /// </summary>
        private readonly WorkOrderServiceDbContext _dbContext;

        /// <summary>
        /// Constructor <c>WorkOrderService</c> is used to instantiate the Work Orders Service.
        /// </summary>
        /// <param name="dbContext">The class representing the database context.</param>
        public ExternalSystemsService(WorkOrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public ExternalSystem Create(ExternalSystem externalSystem)
        {
            _dbContext.ExternalSystems.Add(externalSystem);
            if (_dbContext.SaveChanges() > 1)
                return externalSystem;

            return null;
        }

        // <inheritdoc />
        public async Task<ExternalSystem> CreateAsync(ExternalSystem externalSystem)
        {
            await _dbContext.ExternalSystems.AddAsync(externalSystem);
            if (await _dbContext.SaveChangesAsync() > 1)
                return externalSystem;

            return null;
        }

        // <inheritdoc />
        public bool Delete(string externalSystemId)
        {
            if (!string.IsNullOrWhiteSpace(externalSystemId))
                return false;

            var externalSystem = _dbContext.ExternalSystems.Find(externalSystemId);
            if (externalSystem is null)
                return false;

            _dbContext.ExternalSystems.Remove(externalSystem);
            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> DeleteAsync(string externalSystemId)
        {
            if (!string.IsNullOrWhiteSpace(externalSystemId))
                return false;

            var externalSystem = await _dbContext.ExternalSystems.FindAsync(externalSystemId);
            if (externalSystem is null)
                return false;

            _dbContext.ExternalSystems.Remove(externalSystem);
            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public IEnumerable<ExternalSystem> GetAll()
        {
            IEnumerable<ExternalSystem> externalSystems;
            externalSystems = _dbContext.ExternalSystems.AsEnumerable();

            if (!externalSystems.Any())
                return null;

            return externalSystems;
        }

        // <inheritdoc />
        public Task<IEnumerable<ExternalSystem>> GetAllAsync() => Task.FromResult(_dbContext.ExternalSystems.AsEnumerable());

        // <inheritdoc />
        public ExternalSystem? GetById(string externalSystemId)
        {
            var externalSystem = _dbContext.ExternalSystems.Find(externalSystemId);
            if (externalSystem is null)
                return null;

            return externalSystem;
        }

        // <inheritdoc />
        public async Task<ExternalSystem?> GetByIdAsync(string externalSystemId)
        {
            var externalSystem = await _dbContext.ExternalSystems.FindAsync(externalSystemId);
            if (externalSystem is null)
                return null;

            return externalSystem;
        }

        // <inheritdoc />
        public bool Update(string externalSystemId, ExternalSystem externalSystem)
        {
            var oldExternalSystem = _dbContext.ExternalSystems.Find(externalSystemId);
            if (oldExternalSystem is null)
                return false;

            var newExternalSystem = new ExternalSystem()
            {
                ExternalSystemName = externalSystem.ExternalSystemName,
                ExternalSystemDescription = externalSystem.ExternalSystemDescription,
                ExternalSystemCode = externalSystem.ExternalSystemCode,
                LastModified = DateTime.Now
            };

            // Perhaps add code for the Work Orders & Work Order Events relationships.

            _dbContext.ExternalSystems.Update(newExternalSystem);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string externalSystemId, ExternalSystem externalSystem)
        {
            var oldExternalSystem = await _dbContext.ExternalSystems.FindAsync(externalSystemId);
            if (oldExternalSystem is null)
                return false;

            var newExternalSystem = new ExternalSystem()
            {
                ExternalSystemName = externalSystem.ExternalSystemName,
                ExternalSystemDescription = externalSystem.ExternalSystemDescription,
                ExternalSystemCode = externalSystem.ExternalSystemCode,
                LastModified = DateTime.Now
            };

            // Perhaps add code for the Work Orders & Work Order Events relationships.

            _dbContext.ExternalSystems.Update(newExternalSystem);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
