
using DigitalTwin.WorkOrderProcessor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DigitalTwin.WorkOrderProcessor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .ConfigureServices((context, services) =>
                           {
                               // Register you background worker
                               services.AddHostedService<WorkOrderProcessorWorker>();

                               // Register your database context here
                               services.AddDbContext<WorkOrderProcessorDbContext>(options =>
                                    options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));
                           })
                           .Build();

            await host.RunAsync();
        }
    }
}
