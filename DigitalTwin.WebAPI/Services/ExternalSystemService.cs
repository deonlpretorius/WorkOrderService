using DigitalTwin.WebAPI.Data;
using DigitalTwin.WebAPI.Interfaces;
using DigitalTwin.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Services
{
    /// <summary>
    /// Class <c>SitesService</c> represents the Sites Service.
    /// <remarks>
    /// Implements the ISitesService <see cref="IExternalSystemService"/>
    /// </remarks>
    /// </summary>
    public class ExternalSystemService : IExternalSystemService
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
        public ExternalSystemService(WorkOrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public ExternalSystem Create(ExternalSystem externalSystem)
        {
            if (externalSystem is null)
                throw new Exception("The external system is empty.");

            var newExternalSystem = new ExternalSystem()
            {
                ExternalSystemName = externalSystem.ExternalSystemName,
                ExternalSystemDescription = externalSystem.ExternalSystemDescription,
                ExternalSystemCode = externalSystem.ExternalSystemCode
            };

            _dbContext.ExternalSystems.Add(newExternalSystem);
            if (_dbContext.SaveChanges() > 1)
                return newExternalSystem;

            return externalSystem;
        }

        // <inheritdoc />
        public async Task<ExternalSystem> CreateAsync(ExternalSystem externalSystem)
        {
            if (externalSystem is null)
                throw new Exception("The external system is empty.");

            var newExternalSystem = new ExternalSystem()
            {
                ExternalSystemName = externalSystem.ExternalSystemName,
                ExternalSystemDescription = externalSystem.ExternalSystemDescription,
                ExternalSystemCode = externalSystem.ExternalSystemCode
            };

            await _dbContext.ExternalSystems.AddAsync(newExternalSystem);
            if (await _dbContext.SaveChangesAsync() > 1)
                return newExternalSystem;

            return externalSystem;
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

        public ExternalSystem? GetByCode(string externalSystemCode)
        {
            var externalSystem = _dbContext.ExternalSystems.Where(x => x.ExternalSystemCode == externalSystemCode).FirstOrDefault();
            if (externalSystem is null)
                return null;

            return externalSystem;
        }

        public async Task<ExternalSystem?> GetByCodeAsync(string externalSystemCode)
        {
            var externalSystem = await _dbContext.ExternalSystems.Where(x => x.ExternalSystemCode == externalSystemCode).FirstOrDefaultAsync();
            if (externalSystem is null)
                return null;

            return externalSystem;
        }

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
            var currentExternalSystem = _dbContext.ExternalSystems.Find(externalSystemId);
            if (currentExternalSystem is null)
                return false;

            currentExternalSystem.ExternalSystemName = externalSystem.ExternalSystemName;
            currentExternalSystem.ExternalSystemDescription = externalSystem.ExternalSystemDescription;
            currentExternalSystem.ExternalSystemCode = externalSystem.ExternalSystemCode;
            currentExternalSystem.LastModified = DateTime.Now;

            _dbContext.ExternalSystems.Update(currentExternalSystem);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string externalSystemId, ExternalSystem externalSystem)
        {
            var currentExternalSystem = await _dbContext.ExternalSystems.FindAsync(externalSystemId);
            if (currentExternalSystem is null)
                return false;

            currentExternalSystem.ExternalSystemName = externalSystem.ExternalSystemName;
            currentExternalSystem.ExternalSystemDescription = externalSystem.ExternalSystemDescription;
            currentExternalSystem.ExternalSystemCode = externalSystem.ExternalSystemCode;
            currentExternalSystem.LastModified = DateTime.Now;

            _dbContext.ExternalSystems.Update(currentExternalSystem);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
