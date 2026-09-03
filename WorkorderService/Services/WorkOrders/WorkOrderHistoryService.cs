using DigitalTwin.WorkOrderService.Enums.WorkOrders;
using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Data;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;
using Microsoft.EntityFrameworkCore;

namespace DigitalTwin.WorkOrderService.WebAPI.Services.WorkOrders
{
    public class WorkOrderHistoryService : IWorkOrderHistoryService
    {
        private readonly IWorkOrderStatusService _workOrderStatusService;

        private readonly WorkOrderWebServiceWebAPIDbContext _dbContext;

        public WorkOrderHistoryService(IWorkOrderStatusService workOrderStatusesService, 
                                       WorkOrderWebServiceWebAPIDbContext workOrderServiceDbContext)
        {
            _workOrderStatusService = workOrderStatusesService;
            _dbContext = workOrderServiceDbContext;
        }

        public WorkOrderHistory Create(WorkOrderHistory workOrderHistory)
        {
            if (workOrderHistory is null)
                throw new Exception("The work order history is empty.");

            var newWorkOrderHistory = new WorkOrderHistory();

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrderHistory.WorkOrderStatusId))
                throw new Exception("The work order status for the work order status chamge is empty.");

            var workOrderStatus = _workOrderStatusService.GetById(workOrderHistory.WorkOrderStatusId);
            if (workOrderHistory is null)
                throw new Exception("The work order status history");

            newWorkOrderHistory.WorkOrderStatusId = workOrderHistory.WorkOrderStatusId;
            newWorkOrderHistory.WorkOrderStatus = workOrderStatus;

            // Work Order.
            if (string.IsNullOrWhiteSpace(workOrderHistory.WorkOrderId))
                throw new Exception("The work order is empty.");

            var workOrder = _dbContext.WorkOrders.Find(workOrderHistory.WorkOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not found.");

            newWorkOrderHistory.WorkOrderId = workOrder.WorkOrderId;
            newWorkOrderHistory.WorkOrder = workOrder;

            _dbContext.WorkOrderHistories.Add(newWorkOrderHistory);
            if (_dbContext.SaveChanges() > 0)
                return newWorkOrderHistory;

            return workOrderHistory;
        }

        public async Task<WorkOrderHistory> CreateAsync(WorkOrderHistory workOrderHistory)
        {
            if (workOrderHistory is null)
                throw new Exception("The work order history is empty.");

            var newWorkOrderHistory = new WorkOrderHistory();

            // Work Order Status.
            if (string.IsNullOrWhiteSpace(workOrderHistory.WorkOrderStatusId))
                throw new Exception("The work order status for the work order status chamge is empty.");

            var workOrderStatus = _workOrderStatusService.GetById(workOrderHistory.WorkOrderStatusId);
            if (workOrderHistory is null)
                throw new Exception("The work order status history");

            newWorkOrderHistory.WorkOrderStatusId = workOrderHistory.WorkOrderStatusId;
            newWorkOrderHistory.WorkOrderStatus = workOrderStatus;

            // Work Order.
            if (string.IsNullOrWhiteSpace(workOrderHistory.WorkOrderId))
                throw new Exception("The work order is empty.");

            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderHistory.WorkOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not found.");

            newWorkOrderHistory.WorkOrderId = workOrder.WorkOrderId;
            newWorkOrderHistory.WorkOrder = workOrder;

            await _dbContext.WorkOrderHistories.AddAsync(newWorkOrderHistory);
            if (await _dbContext.SaveChangesAsync() > 0)
                return newWorkOrderHistory;

            return workOrderHistory;
        }

        public IEnumerable<WorkOrderHistory> GetAll()
        {
            var workOrderHistories = _dbContext.WorkOrderHistories.AsEnumerable();

            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public Task<IEnumerable<WorkOrderHistory>> GetAllAsync() => Task.FromResult(_dbContext.WorkOrderHistories.AsEnumerable());

        public WorkOrderHistory? GetById(string workOrderHistoryId)
        {
            var workOrderHistory = _dbContext.WorkOrderHistories.Find(workOrderHistoryId);
            if (workOrderHistory is null)
                throw new Exception("The work order history is empty.");

            return workOrderHistory;
        }

        public async Task<WorkOrderHistory?> GetByIdAsync(string workOrderHistoryId)
        {
            var workOrderHistory = await _dbContext.WorkOrderHistories.FindAsync(workOrderHistoryId);
            if (workOrderHistory is null)
                throw new Exception("The work order history is empty.");

            return workOrderHistory;
        }

        public IEnumerable<WorkOrderHistory> GetByStatus(WorkOrderStatusType status)
        {
            var workOrderStatus = _workOrderStatusService.GetByStatus(status);
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId).ToList();
            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public async Task<IEnumerable<WorkOrderHistory>> GetByStatusAsync(WorkOrderStatusType status)
        {
            var workOrderStatus = await _workOrderStatusService.GetByStatusAsync(status);
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId).ToListAsync();
            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public IEnumerable<WorkOrderHistory> GetByWorkOrderId(string workOrderId)
        {
            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId).ToList();
            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public async Task<IEnumerable<WorkOrderHistory>> GetByWorkOrderIdAsync(string workOrderId)
        {
            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrder.WorkOrderId).ToListAsync();
            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public IEnumerable<WorkOrderHistory> GetByWorkOrderIdAndStatus(string workOrderId, WorkOrderStatusType status)
        {
            var workOrderStatus = _workOrderStatusService.GetByStatus(status);
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            var workOrder = _dbContext.WorkOrders.Find(workOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId 
                                                                         && x.WorkOrderId == workOrder.WorkOrderId)
                                                                  .ToList();

            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }

        public async Task<IEnumerable<WorkOrderHistory>> GetByWorkOrderIdAndStatusAsync(string workOrderId, WorkOrderStatusType status)
        {
            var workOrderStatus = await _workOrderStatusService.GetByStatusAsync(status);
            if (workOrderStatus is null)
                throw new Exception("The work order status could not be found.");

            var workOrder = await _dbContext.WorkOrders.FindAsync(workOrderId);
            if (workOrder is null)
                throw new Exception("The work order could not be found.");

            var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderStatusId == workOrderStatus.WorkOrderStatusId
                                                                         && x.WorkOrderId == workOrder.WorkOrderId)
                                                                  .ToListAsync();

            if (!workOrderHistories.Any())
                return null;

            return workOrderHistories;
        }
    }
}
