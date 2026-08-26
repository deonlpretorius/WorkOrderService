using DigitalTwin.WorkOrderProcessor.Data;
using Microsoft.Extensions.Hosting;

namespace DigitalTwin.WorkOrderProcessor
{
    public class WorkOrderProcessorWorker : BackgroundService
    {
        private readonly WorkOrderProcessorDbContext _dbContext;

        public WorkOrderProcessorWorker(WorkOrderProcessorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
