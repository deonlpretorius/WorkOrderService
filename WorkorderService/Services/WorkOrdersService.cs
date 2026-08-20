using WorkOrderService.Data;
using WorkOrderService.Interfaces;
using WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Services</c> contains the services for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Services
{
    /// <summary>
    /// Class <c>WorkOrderService</c> represents the 
    /// </summary>
    public class WorkOrdersService : IWorkOrdersService
    {
        private readonly WorkOrderServiceDbContext _dbContext;

        public WorkOrdersService(WorkOrderServiceDbContext dbContext)
        {
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
        public IEnumerable<WorkOrder> GetAll()
        {
            IEnumerable<WorkOrder> workOrders;
            workOrders = _dbContext.WorkOrders.AsEnumerable();

            if (!workOrders.Any())
                return workOrders;

            return workOrders;
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrder>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrders.AsEnumerable());

        // <inheritdoc />
        public WorkOrder? GetById(string workOrderId)
        {
            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
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

            return workOrder;
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
                var externalSystem = _dbContext.ExternalSystems.Find(workOrder.ExternalSystem?.ExternalSystemCode);
                if (externalSystem != null)
                {
                    workOrder.ExternalSystem = externalSystem;
                    workOrder.ExternalSystemId = externalSystem.ExternalSystemId;
                }
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
                var workOrderStatus = _dbContext.WorkOrderStatuses.Find(workOrder.WorkOrderStatusId);
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
                var externalSystem = await _dbContext.ExternalSystems.FindAsync(workOrder.ExternalSystem?.ExternalSystemCode);
                if (externalSystem != null)
                {
                    workOrder.ExternalSystem = externalSystem;
                    workOrder.ExternalSystemId = externalSystem.ExternalSystemId;
                }
            }

            // Site
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.SiteId))
            {
                var site = await _dbContext.Sites.FindAsync(workOrder.SiteId);
                if (site is null)
                    return false;

                workOrder.Site = site;
                workOrder.SiteId = site.SiteId;
            }

            // Work Order Status
            if (!string.IsNullOrWhiteSpace(updatedWorkOrder.WorkOrderStatusId))
            {
                var workOrderStatus = await _dbContext.WorkOrderStatuses.FindAsync(workOrder.WorkOrderStatusId);
                if (workOrderStatus is null)
                    return false;

                workOrder.WorkOrderStatus = workOrderStatus;
                workOrder.WorkOrderStatusId = workOrderStatus.WorkOrderStatusId;
            }
            workOrder.LastModified = DateTime.UtcNow;

            if (await _dbContext.SaveChangesAsync() > 1)
                return true;

            return false;
        }
    }
}
