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
    public class WorkOrderService : IWorkOrderService
    {
        private readonly WorkOrderServiceDbContext _dbContext;

        public WorkOrderService(WorkOrderServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // <inheritdoc />
        public WorkOrder Create(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<WorkOrder> CreateAsync(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public bool Delete(string workOrderId)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<bool> DeleteAsync(string workOrderId)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public IEnumerable<WorkOrder> GetAll()
        {
            // var workOrders = _dbContext.WorkOrders
        }

        // <inheritdoc />
        public Task<IEnumerable<WorkOrder>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public WorkOrder? GetById(string workOrderId)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<WorkOrder?> GetByIdAsync(string workOrderId)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public bool Update(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }

        // <inheritdoc />
        public Task<bool> UpdateAsync(WorkOrder workOrder)
        {
            throw new NotImplementedException();
        }
    }
}
