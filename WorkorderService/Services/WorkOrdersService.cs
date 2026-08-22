using Microsoft.EntityFrameworkCore;
using WorkOrderService.Data;
using WorkOrderService.Enums;
using WorkOrderService.Interfaces;
using WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Services
{
    /// <summary>
    /// Class <c>WorkOrderService</c> represents the Work Orders Service.
    /// <remarks>
    /// Implements the IWorkOrdersService <see cref="IWorkOrdersService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrdersService : IWorkOrdersService
    {
        /// <summary>
        /// Property <c>_workOrderStatusesService</c> represents the Work Order Status service.
        /// <value>An interface representing the contract for the work order status service.</value>
        /// </summary>
        private readonly IWorkOrderStatusesService _workOrderStatusesService;

        /// <summary>
        /// Property <c>_sitesService</c> represents the Sites service,
        /// <value>An interface representing the contract for the sites service.</value>
        /// </summary>
        private readonly ISitesService _sitesService;

        /// <summary>
        /// Property <c>_externalSystemsService</c> represents the External Systems service.
        /// <value>An interface representing the contract for the external system service.</value>
        /// </summary>
        private readonly IExternalSystemsService _externalSystemsService;

        /// <summary>
        /// Property <c>WorkOrderServiceDbContext</c> represents the database context.
        /// <value>A class containing the data access layer.</value>
        /// </summary>
        private readonly WorkOrderServiceDbContext _dbContext;

        /// <summary>
        /// Constructor <c>WorkOrderService</c> is used to instantiate the Work Orders Service.
        /// </summary>
        /// <param name="dbContext">The class representing the database context.</param>
        public WorkOrdersService(IWorkOrderStatusesService workOrderStatusesService, ISitesService sitesService, IExternalSystemsService externalSystemsService, WorkOrderServiceDbContext dbContext)
        {
            _workOrderStatusesService = workOrderStatusesService;
            _sitesService = sitesService;
            _externalSystemsService = externalSystemsService;
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrder Create(WorkOrder workOrder)
        {
            _dbContext.WorkOrders.Add(workOrder);
            if (_dbContext.SaveChanges() == 0)
                return workOrder;

            return workOrder;
        }

        // <inheritdoc />
        public async Task<WorkOrder> CreateAsync(WorkOrder workOrder)
        {
            await _dbContext.WorkOrders.AddAsync(workOrder);
            if (await _dbContext.SaveChangesAsync() > 1)
                return workOrder;

            return workOrder;
        }

        // <inheritdoc />
        public bool Delete(string workOrderId)
        {
            if (string.IsNullOrWhiteSpace(workOrderId))
                return false;

            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
            if (workOrder is null)
                return false;

            _dbContext.WorkOrders.Remove(workOrder);
            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> DeleteAsync(string workOrderId)
        {
            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);
            if (workOrder is null)
                return false;

            _dbContext.WorkOrders.Remove(workOrder);
            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrder>> GetAll()
        {
            var workOrders = _dbContext.WorkOrders.AsEnumerable();

            // Make sure to return a minimal status change history with a single work order.
            foreach (var workOrder in workOrders)
            {
                var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId)
                                                                            .Take(10)
                                                                            .ToList();
                workOrder.WorkOrderHistories = workOrderHistories;
            }

            if (workOrders.Any())
                return (Task<IEnumerable<WorkOrder>>)workOrders;

            return null;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrder>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrders.AsEnumerable());

        // <inheritdoc />
        public WorkOrder? GetById(string workOrderId)
        {
            var workOrder = _dbContext.WorkOrders.Find(workOrderId);

            if (workOrder is null)
                return null;

            // Make sure to return a minimal status change history with a single work order.
            var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId).ToList();
            workOrder.WorkOrderHistories = workOrderHistories;

            if (workOrder is null)
                return null;

            return workOrder;
        }

        // <inheritdoc />
        public async Task<WorkOrder?> GetByIdAsync(string workOrderId)
        {
            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);

            if (workOrder is null)
                return null;

            // Return a minimal status change history with a single work order.
            var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId).ToListAsync();
            workOrder.WorkOrderHistories = workOrderHistories;

            if (workOrder is null)
                return null;

            return workOrder;
        }

        // <inheritdoc />
        public IEnumerable<WorkOrder> GetByStatus(WorkOrderStatusType status, int pageNumber, int pageSize)
        {
            // Retrieve the work order status first.
            var workOrderStatus = _dbContext.WorkOrderStatuses.Where(x => x.Status == status).FirstOrDefault();

            // Retrieve all of the work orders by the respective status
            if (workOrderStatus is null && workOrderStatus?.Status != status)
                return null;

            // Make sure to keep to a fixed page size.
            var workOrders = _dbContext.WorkOrders.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId)
                                                  .Skip((pageNumber - 1) * pageSize)
                                                  .Take(pageSize)
                                                  .ToList();

            // Maintain a minimal status change history with a single work order.
            foreach(var workOrder in workOrders)
            {
                var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId)
                                                                      .Take(5)
                                                                      .ToList();
                workOrder.WorkOrderHistories = workOrderHistories;
            }

            if (workOrders.Any())
                return workOrders;

            return null;
        }

        public async Task<IEnumerable<WorkOrder>> GetByStatusAsync(WorkOrderStatusType status, int pageNumber, int pageSize)
        {
            // Retrieve the work order status first.
            var workOrderStatus = _dbContext.WorkOrderStatuses.Where(x => x.Status == status).FirstOrDefault();

            // Retrieve all of the work orders by the respective status
            if (workOrderStatus is null && workOrderStatus?.Status != status)
                return null;

            // Make sure to keep to a fixed page size.
            var workOrders = await _dbContext.WorkOrders.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId)
                                                        .Skip((pageNumber - 1) * pageSize)
                                                        .Take(pageSize)
                                                        .ToListAsync();

            // Maintain a minimal status change history along with a single work order.
            foreach (var workOrder in workOrders)
            {
                var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId)
                                                                            .Take(5)
                                                                            .ToListAsync();
                workOrder.WorkOrderHistories = workOrderHistories;
            }

            if (!workOrders.Any())
                return workOrders;

            return null;
        }

        // <inheritdoc />
        public bool Update(string workOrderId, WorkOrder updatedWorkOrder)
        {
            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
            if (workOrder is null)
                return false;

            workOrder.WorkOrderName = updatedWorkOrder.WorkOrderName;
            workOrder.WorkOrderDescription = updatedWorkOrder.WorkOrderDescription;
            workOrder.WorkOrderExternalId = updatedWorkOrder.WorkOrderExternalId;

            // External System
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.ExternalSystemId))
            {
                var externalSystem = _externalSystemsService.GetById(updatedWorkOrder.ExternalSystemId);
                workOrder.ExternalSystem = externalSystem;
                workOrder.ExternalSystemId = externalSystem?.ExternalSystemId;
            }

            // Site
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.SiteId))
            {
                var site = _dbContext.Sites.Find(workOrder.SiteId);
                if (site is null)
                    return false;

                workOrder.Site = site;
                workOrder.SiteId = site.SiteId;
            }

            // Work Order Status
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.WorkOrderStatusId))
            {
                var workOrderStatus = _workOrderStatusesService.GetById(updatedWorkOrder.WorkOrderStatusId);
                if (workOrderStatus is null)
                    return false;

                workOrder.WorkOrderStatus = workOrderStatus;
                workOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            }
            workOrder.LastModified = DateTime.UtcNow;

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateAsync(string workOrderId, WorkOrder updatedWorkOrder)
        {
            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);
            if (workOrder is null)
                return false;

            workOrder.WorkOrderName = updatedWorkOrder.WorkOrderName;
            workOrder.WorkOrderDescription = updatedWorkOrder.WorkOrderDescription;
            workOrder.WorkOrderExternalId = updatedWorkOrder.WorkOrderExternalId;

            // External System
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.ExternalSystemId))
            {
                var externalSystem = await _externalSystemsService.GetByIdAsync(updatedWorkOrder.ExternalSystemId);
                workOrder.ExternalSystem = externalSystem;
                workOrder.ExternalSystemId = externalSystem?.ExternalSystemId;
            }

            // Site
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.SiteId))
            {
                var site = await _sitesService.GetByIdAsync(updatedWorkOrder.SiteId);
                if (site is null)
                    return false;

                workOrder.Site = site;
                workOrder.SiteId = site.SiteId;
            }

            // Work Order Status
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.WorkOrderStatusId))
            {
                var workOrderStatus = await _workOrderStatusesService.GetByIdAsync(updatedWorkOrder.WorkOrderStatusId);
                if (workOrderStatus is null)
                    return false;

                workOrder.WorkOrderStatus = workOrderStatus;
                workOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            }
            workOrder.LastModified = DateTime.Now;

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public bool UpdateWorkOrderStatus(string workOrderId, WorkOrderStatusType status)
        {
            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
            if (workOrder is null)
                return false;

            // Check if the supplied work order status exists in the database.
            var workOrderStatus = _dbContext.WorkOrderStatuses.Where(s => s.Status == status).FirstOrDefault();
            if (workOrderStatus is null)
                return false;

            workOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            workOrder.WorkOrderStatus = workOrderStatus;

            _dbContext.WorkOrders.Update(workOrder);

            // Add the status change to the work order history table for record keeping purposes.
            var workOrderHistory = new WorkOrderHistory()
            {
                WorkOrderId = workOrderId,
                WorkOrder = workOrder,
                WorkOrderStatusId = workOrderStatus.WorkOrderStatusId,
                WorkOrderStatus = workOrderStatus,
                UpdatedAt = DateTime.Now
            };
            _dbContext.WorkOrderHistories.Add(workOrderHistory);

            if (_dbContext.SaveChanges() > 1)
                return true;

            return false;
        }

        // <inheritdoc />
        public async Task<bool> UpdateWorkOrderStatusAsync(string workOrderId, WorkOrderStatusType status)
        {
            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);
            if (workOrder is null)
                return false;

            // Check if the supplied work order status exists in the database.
            var workOrderStatus = _dbContext.WorkOrderStatuses.Where(s => s.Status == status).FirstOrDefault();
            if (workOrderStatus is null)
                return false;

            workOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            workOrder.WorkOrderStatus = workOrderStatus;

            _dbContext.WorkOrders.Update(workOrder);

            // Add the status change to the work order history table for record keeping purposes.
            var workOrderHistory = new WorkOrderHistory()
            {
                WorkOrderId = workOrderId,
                WorkOrder = workOrder,
                WorkOrderStatusId = workOrderStatus.WorkOrderStatusId,
                WorkOrderStatus = workOrderStatus,
                UpdatedAt = DateTime.Now
            };
            _dbContext.WorkOrderHistories.Add(workOrderHistory);

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
