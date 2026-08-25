using DigitalTwin.WebAPI.Data;
using DigitalTwin.WebAPI.Enums.WorkOrders;
using DigitalTwin.WebAPI.Interfaces;
using DigitalTwin.WebAPI.Interfaces.WorkOrders;
using DigitalTwin.WebAPI.Models.WorkOrders;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Services.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderService</c> represents the Work Orders Service.
    /// <remarks>
    /// Implements the IWorkOrdersService <see cref="IWorkOrderService"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderService : IWorkOrderService
    {
        /// <summary>
        /// Property <c>_workOrderStatusesService</c> represents the Work Order Status service.
        /// <value>An interface representing the contract for the work order status service.</value>
        /// </summary>
        private readonly IWorkOrderStatusService _workOrderStatusService;

        /// <summary>
        /// Property <c>_sitesService</c> represents the Sites service,
        /// <value>An interface representing the contract for the sites service.</value>
        /// </summary>
        private readonly ISiteService _sitesService;

        /// <summary>
        /// Property <c>_externalSystemsService</c> represents the External Systems service.
        /// <value>An interface representing the contract for the external system service.</value>
        /// </summary>
        private readonly IExternalSystemService _externalSystemsService;

        /// <summary>
        /// Property <c>_workOrderHistoryService</c> represents the Work Order History service.
        /// <value>An interface representing the contract for the work order history service.</value>
        /// </summary>
        private readonly IWorkOrderHistoryService _workOrderHistoryService;

        /// <summary>
        /// Property <c>WorkOrderServiceDbContext</c> represents the database context.
        /// <value>A class containing the data access layer.</value>
        /// </summary>
        private readonly WorkOrderServiceDbContext _dbContext;

        /// <summary>
        /// Constructor <c>WorkOrderService</c> is used to instantiate the Work Orders Service.
        /// </summary>
        /// <param name="dbContext">The class representing the database context.</param>
        public WorkOrderService(IWorkOrderStatusService workOrderStatusesService, 
                                ISiteService sitesService, 
                                IExternalSystemService externalSystemsService,
                                IWorkOrderHistoryService workOrderHistoryService,
                                WorkOrderServiceDbContext dbContext)
        {
            _workOrderStatusService = workOrderStatusesService;
            _sitesService = sitesService;
            _externalSystemsService = externalSystemsService;
            _workOrderHistoryService = workOrderHistoryService;
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrder Create(WorkOrder workOrder)
        {
            if (workOrder is null)
                throw new Exception("Could not create a new work order. The work order is empty.");

            var newWorkOrder = new WorkOrder()
            {
                WorkOrderName = workOrder.WorkOrderName,
                WorkOrderDescription = workOrder.WorkOrderDescription,
                WorkOrderExternalId = workOrder.WorkOrderExternalId,
            };

            // External System.
            if (!string.IsNullOrWhiteSpace(workOrder.WorkOrderExternalId))
            {
                if (!string.IsNullOrWhiteSpace(workOrder.ExternalSystemId))
                {
                    var externalSystem = _externalSystemsService.GetById(workOrder.ExternalSystemId);
                    if (externalSystem is not null)
                    {
                        newWorkOrder.ExternalSystemId = externalSystem.ExternalSystemId;
                        newWorkOrder.ExternalSystem = externalSystem;
                    }
                }
            }

            // Site
            if (string.IsNullOrWhiteSpace(workOrder.SiteId))
                throw new Exception("The site for the work order is empty.");

            var site = _sitesService.GetById(workOrder.SiteId);
            if (site is null)
                throw new Exception("The site for the work order could not be found.");

            newWorkOrder.SiteId = site.SiteId;
            newWorkOrder.Site = site;

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrder.WorkOrderStatusId))
                throw new Exception("The status for the work order is empty.");

            var workOrderStatus = _workOrderStatusService.GetById(workOrder.WorkOrderStatusId);
            if (workOrderStatus is null)
                throw new Exception("The work order status for the work order could not be found.");

            newWorkOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            newWorkOrder.WorkOrderStatus = workOrderStatus;

            _dbContext.WorkOrders.Add(workOrder);
            if (_dbContext.SaveChanges() == 0)
                return workOrder;

            return workOrder;
        }

        // <inheritdoc />
        public async Task<WorkOrder> CreateAsync(WorkOrder workOrder)
        {
            if (workOrder is null)
                throw new Exception("Could not create a new work order. The work order is empty.");

            var newWorkOrder = new WorkOrder()
            {
                WorkOrderName = workOrder.WorkOrderName,
                WorkOrderDescription = workOrder.WorkOrderDescription,
                WorkOrderExternalId = workOrder.WorkOrderExternalId,
            };

            // External System.
            if (!string.IsNullOrWhiteSpace(workOrder.WorkOrderExternalId))
            {
                if (!string.IsNullOrWhiteSpace(workOrder.ExternalSystemId))
                {
                    var externalSystem = await _externalSystemsService.GetByIdAsync(workOrder.ExternalSystemId);
                    if (externalSystem is not null)
                    {
                        newWorkOrder.ExternalSystemId = externalSystem.ExternalSystemId;
                        newWorkOrder.ExternalSystem = externalSystem;
                    }
                }
            }

            // Site
            if (string.IsNullOrWhiteSpace(workOrder.SiteId))
                throw new Exception("The site for the work order is empty.");

            var site = await _sitesService.GetByIdAsync(workOrder.SiteId);
            if (site is null)
                throw new Exception("The site for the work order could not be found.");

            newWorkOrder.SiteId = site.SiteId;
            newWorkOrder.Site = site;

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrder.WorkOrderStatusId))
                throw new Exception("The status for the work order is empty.");

            var workOrderStatus = await _workOrderStatusService.GetByIdAsync(workOrder.WorkOrderStatusId);
            if (workOrderStatus is null)
                throw new Exception("The work order status for the work order could not be found.");

            newWorkOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            newWorkOrder.WorkOrderStatus = workOrderStatus;

            await _dbContext.WorkOrders.AddAsync(workOrder);
            if (await _dbContext.SaveChangesAsync() == 0)
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
        public IEnumerable<WorkOrder> GetAll()
        {
            var workOrders = _dbContext.WorkOrders.AsEnumerable();

            // Make sure to return a minimal status change history with a single work order.
            if (workOrders.Any())
            {
                foreach (var workOrder in workOrders)
                {
                    var workOrderHistories = _workOrderHistoryService.GetByWorkOrderId(workOrder?.WorkOrderId)
                                                                     .Take(10)
                                                                     .ToList();

                    workOrder.WorkOrderHistories = workOrderHistories;
                }

            }

            if (workOrders.Any())
                return workOrders;

            return null;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrder>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrders.AsEnumerable());

        public WorkOrder? GetByExternalId(string workOrderExternalId)
        {
            if (string.IsNullOrWhiteSpace(workOrderExternalId))
                throw new Exception("The work order external identifier is empty.");

            var workOrder = _dbContext.WorkOrders.Where(x => x.ExternalSystemId == workOrderExternalId).FirstOrDefault();
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            return workOrder;
        }

        public async Task<WorkOrder?> GetByExternalIdAsync(string workOrderExternalId)
        {
            if (string.IsNullOrWhiteSpace(workOrderExternalId))
                throw new Exception("The work order external identifier is empty.");

            var workOrder = await _dbContext.WorkOrders.Where(x => x.ExternalSystemId == workOrderExternalId).FirstOrDefaultAsync();
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            return workOrder;
        }

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

            if (!workOrders.Any())
                return null;

            return workOrders;
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
                return null;

            return workOrders;
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
                var workOrderStatus = _workOrderStatusService.GetById(updatedWorkOrder.WorkOrderStatusId);
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
                var workOrderStatus = await _workOrderStatusService.GetByIdAsync(updatedWorkOrder.WorkOrderStatusId);
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
