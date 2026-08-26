using DigitalTwin.Models.WorkOrders;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Namespace <c>DigitalTwin.WorkOrderProcessor.Data</c> contains the database access layer operations for the application.
/// </summary>
namespace DigitalTwin.WorkOrderProcessor.Data
{
    /// <summary>
    /// Class <c>WorkOrderProcessorDbContext</c> represents the data access layer context for the Work Order Processor.
    /// <remarks>
    /// Inherits from DbContext <see cref="DbContext"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderProcessorDbContext : DbContext
    {
        public WorkOrderProcessorDbContext(DbContextOptions<WorkOrderProcessorDbContext> options) : base(options)
        {
            
        }

        public DbSet<WorkOrderEvent> WorkOrderEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
