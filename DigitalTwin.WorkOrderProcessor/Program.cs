using DigitalTwin.WorkOrderProcessor.Interfaces;
using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WorkOrderProcessor.Data;
using DigitalTwin.WorkOrderService.WorkOrderProcessor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalTwin.WorkOrderService.WorkOrderProcessor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .ConfigureServices((context, services) =>
                           {
                               // Register you background worker
                               services.AddSingleton<IQueueService<WorkOrderEvent>, QueueService<WorkOrderEvent>>();
                               services.AddHostedService<WorkOrderProcessorService>();

                               // Register your database context here
                               services.AddDbContext<WorkOrderProcessorDbContext>(options =>
                                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                           }).Build();

            await host.RunAsync();
        }
    }
}
