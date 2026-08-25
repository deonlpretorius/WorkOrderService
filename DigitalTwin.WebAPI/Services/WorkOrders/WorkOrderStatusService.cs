using DigitalTwin.WebAPI.Data;
using DigitalTwin.WebAPI.Enums.WorkOrders;
using DigitalTwin.WebAPI.Interfaces.WorkOrders;
using DigitalTwin.WebAPI.Models.WorkOrders;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Services.WorkOrders
{
    /// <summary>
    /// Class <c>SitesService</c> represents the Sites Service.
    /// <remarks>
    /// Implements the IWorkOrderStatusService <see cref="IWorkOrderStatusService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderStatusService : IWorkOrderStatusService
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
        public WorkOrderStatusService(WorkOrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrderStatus Create(WorkOrderStatus workOrderStatus)
        {
            if (workOrderStatus is null)
                throw new Exception("The work order status is empty.");

            var newWorkOrderStatus = new WorkOrderStatus()
            {
                WorkOrderStatusName = workOrderStatus.WorkOrderStatusName,
                WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription,
                Status = workOrderStatus.Status
            };

            _dbContext.WorkOrderStatuses.Add(newWorkOrderStatus);
            if (_dbContext.SaveChanges() > 1)
                return newWorkOrderStatus;

            return workOrderStatus;
        }

        // <inheritdoc />
        public async Task<WorkOrderStatus> CreateAsync(WorkOrderStatus workOrderStatus)
        {
            if (workOrderStatus is null)
                throw new Exception("The work order status is empty.");

            var newWorkOrderStatus = new WorkOrderStatus()
            {
                WorkOrderStatusName = workOrderStatus.WorkOrderStatusName,
                WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription,
                Status = workOrderStatus.Status
            };

            _dbContext.WorkOrderStatuses.Add(newWorkOrderStatus);
            if (await _dbContext.SaveChangesAsync() > 1)
                return newWorkOrderStatus;

            return workOrderStatus;
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

        public WorkOrderStatus? GetByStatus(WorkOrderStatusType status)
        {
            var workOrderStatus = _dbContext.WorkOrderStatuses.Where(x => x.Status == status).FirstOrDefault();
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            return workOrderStatus;
        }

        public async Task<WorkOrderStatus?> GetByStatusAsync(WorkOrderStatusType status)
        {
            var workOrderStatus = await _dbContext.WorkOrderStatuses.Where(x => x.Status == status).FirstOrDefaultAsync();
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            return workOrderStatus;
        }

        // <inheritdoc />
        public bool Update(string workOrderStatusId, WorkOrderStatus workOrderStatus)
        {
            var currentWorkOrderStatus = _dbContext.WorkOrderStatuses.Find(workOrderStatusId);
            if (currentWorkOrderStatus is null)
                return false;

            currentWorkOrderStatus.WorkOrderStatusName = workOrderStatus.WorkOrderStatusName;
            currentWorkOrderStatus.WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription;
            currentWorkOrderStatus.Status = workOrderStatus.Status;

            _dbContext.WorkOrderStatuses.Update(currentWorkOrderStatus);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string workOrderStatusId, WorkOrderStatus workOrderStatus)
        {
            var currentWorkOrderStatus = await _dbContext.WorkOrderStatuses.FindAsync(workOrderStatusId);
            if (currentWorkOrderStatus is null)
                return false;

            currentWorkOrderStatus.WorkOrderStatusName = workOrderStatus.WorkOrderStatusName;
            currentWorkOrderStatus.WorkOrderStatusDescription = workOrderStatus.WorkOrderStatusDescription;
            currentWorkOrderStatus.Status = workOrderStatus.Status;

            _dbContext.WorkOrderStatuses.Update(currentWorkOrderStatus);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
