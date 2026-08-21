using WorkOrderService.Data;
using WorkOrderService.Interfaces;
using WorkOrderService.Models.WorkOrders;

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
    public class WorkOrderStatusesService : IWorkOrderStatusesService
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
        public WorkOrderStatusesService(WorkOrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrderStatus Create(WorkOrderStatus workOrderStatus)
        {
            _dbContext.WorkOrderStatuses.Add(workOrderStatus);
            if (_dbContext.SaveChanges() > 1)
                return workOrderStatus;

            return null;
        }

        // <inheritdoc />
        public async Task<WorkOrderStatus> CreateAsync(WorkOrderStatus workOrderStatus)
        {
            await _dbContext.WorkOrderStatuses.AddAsync(workOrderStatus);
            if (await _dbContext.SaveChangesAsync() > 1)
                return workOrderStatus;

            return null;
        }

        // <inheritdoc />
        public bool Delete(string workOrderStatusId)
        {
            if (!string.IsNullOrWhiteSpace(workOrderStatusId))
                return false;

            var workOrderStatus = _dbContext.ExternalSystems.Find(workOrderStatusId);
            if (workOrderStatus is null)
                return false;

            _dbContext.ExternalSystems.Remove(workOrderStatus);
            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> DeleteAsync(string workOrderStatusId)
        {
            if (!string.IsNullOrWhiteSpace(workOrderStatusId))
                return false;

            var externalSystem = await _dbContext.ExternalSystems.FindAsync(workOrderStatusId);
            if (externalSystem is null)
                return false;

            _dbContext.ExternalSystems.Remove(externalSystem);
            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public IEnumerable<WorkOrderStatus> GetAll()
        {
            IEnumerable<WorkOrderStatus> workOrderStatuses;
            workOrderStatuses = _dbContext.WorkOrderStatuses.AsEnumerable();

            if (!workOrderStatuses.Any())
                return null;

            return workOrderStatuses;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrderStatus>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrderStatuses.AsEnumerable());

        // <inheritdoc />
        public WorkOrderStatus? GetById(string workOrderStatusId)
        {
            var workOrderStatus = _dbContext.WorkOrderStatuses.Find(workOrderStatusId);
            if (workOrderStatus is null)
                return null;

            return workOrderStatus;
        }

        // <inheritdoc />
        public async Task<WorkOrderStatus?> GetByIdAsync(string workOrderStatusId)
        {
            var workOrderStatus = await _dbContext.WorkOrderStatuses.FindAsync(workOrderStatusId);
            if (workOrderStatus is null)
                return null;

            return workOrderStatus;
        }

        // <inheritdoc />
        public bool Update(string workOrderStatusId, WorkOrderStatus workOrderStatus)
        {
            var oldWorkOrderStatus = _dbContext.ExternalSystems.Find(workOrderStatusId);
            if (oldWorkOrderStatus is null)
                return false;

            var newWorkOrderStatus = new WorkOrderStatus()
            {
                WorkOrderStatusName = workOrderStatus.WorkOrderStatusName,
                WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription,
                Status = workOrderStatus.Status
            };

            // Perhaps add code for the Work Orders, Work Order Histories, and Work Order Events relationships.

            _dbContext.WorkOrderStatuses.Update(newWorkOrderStatus);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string workOrderStatusId, WorkOrderStatus workOrderStatus)
        {
            var oldWorkOrderStatus = await _dbContext.WorkOrderStatuses.FindAsync(workOrderStatusId);
            if (oldWorkOrderStatus is null)
                return false;

            var newWorkOrderStatus = new WorkOrderStatus()
            {
                WorkOrderStatusName = workOrderStatus.WorkOrderStatusName,
                WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription,
                Status = workOrderStatus.Status
            };

            // Perhaps add code for the Work Orders, Work Order Histories, and Work Order Events relationships.

            _dbContext.WorkOrderStatuses.Update(newWorkOrderStatus);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
