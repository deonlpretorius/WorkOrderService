using Microsoft.EntityFrameworkCore;
using WorkOrderService.Models;
using WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Data</c> contains the data access layer operations for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Data
{
    /// <summary>
    /// Class <c>WorkOrderServiceDbContext</c> represents the database access layer operations.
    /// <remarks>
    /// Inherits from DbContext <see cref="DbContext"/>
    /// </remarks>
    /// </summary>
    public class WorkOrderServiceDbContext : DbContext
    {
        /// <summary>
        /// Constructor <c>WorkOrderServiceDbContext</c> is used to instantiate the data access layer with options.
        /// </summary>
        /// <param name="options">The options for the data access layer context.</param>
        public WorkOrderServiceDbContext(DbContextOptions<WorkOrderServiceDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Property <c>SiteCodes</c> represents the site codes table.
        /// </summary>
        public DbSet<SiteCode> SiteCodes { get; set; }

        /// <summary>
        /// Property <c>ExternalSystem</c> represents the external systems table.
        /// </summary>
        public DbSet<ExternalSystem> ExternalSystems {  get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatuses</c> represents the work order statuses table.
        /// </summary>
        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }

        /// <summary>
        /// Property <c>WorkOrders</c> represents the work orders table.
        /// </summary>
        public DbSet<WorkOrder> WorkOrders { get; set; }

        /// <summary>
        /// Property <c>WorkOrderHistories</c> represents the work order histories table.
        /// </summary>
        public DbSet<WorkOrderHistory> WorkOrderHistories {  get; set; }

        /// <summary>
        /// Property <c>WorkOrderEvents</c> represents the work order events table.
        /// </summary>
        public DbSet<WorkOrderEvent> WorkOrderEvents { get; set; }

        /// <summary>
        /// Method <c>OnModelCreating</c> is used to configure the data models.
        /// </summary>
        /// <param name="modelBuilder">The class containing the model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
