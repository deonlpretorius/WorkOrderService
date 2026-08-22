using Microsoft.EntityFrameworkCore;
using WorkOrderService.Data;
using WorkOrderService.Interfaces;
using WorkOrderService.Models.WorkOrders;

namespace WorkOrderService.Services
{
    public class WorkOrderHistoriesService : IWorkOrderHistoriesService
    {
        private readonly IWorkOrdersService _workOrderService;

        private readonly IWorkOrderStatusesService _workOrderStatusesService;

        private readonly WorkOrderServiceDbContext _dbContext;

        public WorkOrderHistoriesService(IWorkOrdersService workOrderService, IWorkOrderStatusesService workOrderStatusesService, WorkOrderServiceDbContext workOrderServiceDbContext)
        {
            _workOrderService = workOrderService;
            _workOrderStatusesService = workOrderStatusesService;
            _dbContext = workOrderServiceDbContext;
        }

        public void Create(params WorkOrderHistory[] workOrderHistories)
        {
            // Iterate over the work order history entries and each add them into the database.
            _dbContext.WorkOrderHistories.AddRange(workOrderHistories);
            _dbContext.SaveChanges();
        }

        public async void CreateAsync(params WorkOrderHistory[] workOrderHistories)
        {
            // Iterate over the work order history entries and each add them into the database.
            await _dbContext.WorkOrderHistories.AddRangeAsync(workOrderHistories);
            await _dbContext.SaveChangesAsync();
        }

        public IEnumerable<WorkOrderHistory> GetByWorkOrderId(string workOrderId)
        {
            var workOrderHistories = _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrderId).ToList();
            return workOrderHistories;
        }

        public async Task<IEnumerable<WorkOrderHistory>> GetByWorkOrderIdAsync(string workOrderId)
        {
            var workOrderHistories = await _dbContext.WorkOrderHistories.Where(x => x.WorkOrderId == workOrderId).ToListAsync();
            return workOrderHistories;
        }
    }
}
