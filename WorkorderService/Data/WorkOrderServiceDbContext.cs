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
        public DbSet<Site> Sites { get; set; }

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

            // Work Order Statuses.
            // Work Order Statuses - Work Orders.
            modelBuilder.Entity<WorkOrderStatus>()
                        .HasMany(w => w.WorkOrders)
                        .WithOne(s => s.WorkOrderStatus)
                        .HasForeignKey(s => s.WorkOrderStatusId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Work Order Statuses - Work Order History.
            modelBuilder.Entity<WorkOrderStatus>()
                        .HasMany(wh => wh.WorkOrderHistories)
                        .WithOne(s => s.WorkOrderStatus)
                        .OnDelete(DeleteBehavior.Restrict);

            // Work Order Statuses - Work Order Events.
            modelBuilder.Entity<WorkOrderStatus>()
                        .HasMany(we => we.WorkOrderEvents)
                        .WithOne(ws => ws.WorkOrderStatus)
                        .HasForeignKey(ws => ws.WorkOrderStatusId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Sites.
            // Sites - Work Orders.
            modelBuilder.Entity<Site>()
                        .HasMany(w => w.WorkOrders)
                        .WithOne(s => s.Site)
                        .HasForeignKey(s => s.SiteId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Sites - Work Order Events.
            modelBuilder.Entity<Site>()
                        .HasMany(we => we.WorkOrderEvents)
                        .WithOne(s => s.Site)
                        .HasForeignKey(s => s.SiteId)
                        .OnDelete(DeleteBehavior.Restrict);

            // External Systems.
            // External Systems - Work Orders.
            modelBuilder.Entity<ExternalSystem>()
                        .HasMany(w => w.WorkOrders)
                        .WithOne(e => e.ExternalSystem)
                        .HasForeignKey(e => e.ExternalSystemId)
                        .OnDelete(DeleteBehavior.Restrict);

            // External Systems - Work Order Events
            modelBuilder.Entity<ExternalSystem>()
                        .HasMany(we => we.WorkOrderEvents)
                        .WithOne(e => e.ExternalSystem)
                        .HasForeignKey(e => e.ExternalSystemId)
                        .OnDelete(DeleteBehavior.Restrict);

            // External Systems - Work Order Events.
            modelBuilder.Entity<ExternalSystem>()
                        .HasMany(we => we.WorkOrderEvents)
                        .WithOne(e => e.ExternalSystem)
                        .HasForeignKey(e => e.ExternalSystemId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Work Orders.
            // Work Orders - External Systems.
            modelBuilder.Entity<WorkOrder>()
                        .HasOne(s => s.ExternalSystem)
                        .WithMany(w => w.WorkOrders)
                        .HasForeignKey(w => w.ExternalSystemId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Orders - Sites
            modelBuilder.Entity<WorkOrder>()
                        .HasOne(s => s.Site)
                        .WithMany(w => w.WorkOrders)
                        .HasForeignKey(w => w.SiteId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Orders - Work Order Statuses
            modelBuilder.Entity<WorkOrder>()
                        .HasOne(s => s.WorkOrderStatus)
                        .WithMany(w => w.WorkOrders)
                        .HasForeignKey(w => w.WorkOrderStatusId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Orders - Work Order Histories
            modelBuilder.Entity<WorkOrder>()
                        .HasMany(wh => wh.WorkOrderHistories)
                        .WithOne(w => w.WorkOrder)
                        .HasForeignKey(w => w.WorkOrderHistoryId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Work Orders - Work Order Events.
            modelBuilder.Entity<WorkOrder>()
                        .HasMany(we => we.WorkOrderEvents)
                        .WithOne(w => w.WorkOrder)
                        .HasForeignKey(w => w.WorkOrderId)
                        .OnDelete(DeleteBehavior.Cascade);

            // Work Order Histories
            modelBuilder.Entity<WorkOrderHistory>()
                        .HasOne(w => w.WorkOrder)
                        .WithMany(wh => wh.WorkOrderHistories)
                        .HasForeignKey(wh => wh.WorkOrderHistoryId)
                        .OnDelete(DeleteBehavior.Restrict);

            // Work Order Events  // shit
            // Work Order Events - Work Orders
            modelBuilder.Entity<WorkOrderEvent>()
                        .HasOne(w => w.WorkOrder)
                        .WithMany(we => we.WorkOrderEvents)
                        .HasForeignKey(we => we.WorkOrderEventId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Order Events - Sites
            modelBuilder.Entity<WorkOrderEvent>()
                        .HasOne(s => s.Site)
                        .WithMany(we => we.WorkOrderEvents)
                        .HasForeignKey(we => we.WorkOrderEventId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Order Events - Work Order Statuses
            modelBuilder.Entity<WorkOrderEvent>()
                        .HasOne(s => s.WorkOrderStatus)
                        .WithMany(we => we.WorkOrderEvents)
                        .HasForeignKey(we => we.WorkOrderEventId)
                        .OnDelete(DeleteBehavior.NoAction);

            // Work Order Events - External Systems
            modelBuilder.Entity<WorkOrderEvent>()
                        .HasOne(e => e.ExternalSystem)
                        .WithMany(we => we.WorkOrderEvents)
                        .HasForeignKey(we => we.WorkOrderEventId)
                        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
