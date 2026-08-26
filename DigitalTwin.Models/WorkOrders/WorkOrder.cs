using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>DigitalTwin.Models.WorkOrders</c> contains the data models that represent the database tables.
/// </summary>
namespace DigitalTwin.Models.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrder</c> represents the work orders table.
    /// </summary>
    [Table("WorkOrders")]
    public class WorkOrder
    {
        /// <summary>
        /// Constructor <c>WorkOrder</c> is used to instantiate the work order model.
        /// </summary>
        public WorkOrder()
        {
            WorkOrderId = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            LastModified = DateTime.Now;
        }

        /// <summary>
        /// Property <c>WorkOrderId</c> represents the identifier for the work orders table.
        /// <value>A string containing the work orders identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? WorkOrderId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderName</c> represents the name of the work order.
        /// <value>A string containing the work order name. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? WorkOrderName { get; set; }

        /// <summary>
        /// Property <c>WorkOrderDescription</c> represents the description of the work order.
        /// <value>A string containing the work order description. Default is Empty.</value>
        /// </summary>
        [StringLength(250)]
        public string? WorkOrderDescription { get; set; }

        /// <summary>
        /// Property <c>WorkOrderExternalId</c> represents an external identifier for the work order.
        /// This identifier is used to identify a work order from an external system.
        /// <value>A string containing the work order external identifier. Default is Empty.</value>
        /// </summary>
        public string? WorkOrderExternalId { get; set; }

        /// <summary>
        /// Property <c>CreatedAt</c> representing the creation date and time of the work order.
        /// <value>A datetime containing the creation date and time of the work order. Default is DateTime.Now.</value>
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Property <c>LastModified</c> represents the last modofication date and time of the work order.
        /// <value>A datetime containing the work order last modification date and time. Default is DateTime.Now.</value>
        /// </summary>
        [Required]
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Property <c>ExternalId</c> represents the identifier for the work order in an external system.
        /// <value>A string containing the external system identifier. Default is Empty.</value>
        /// </summary>
        [ForeignKey("ExternalSystemId")]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// Property <c>ExternalSystem</c> represents the reference navigation for the external systems table.
        /// <value>A class containing the external system data model. Default is Null.</value>
        /// </summary>
        public ExternalSystem? ExternalSystem { get; set; }

        /// <summary>
        /// Property <c>SiteCodesId</c> represents the identifier for the sites table.
        /// <value>A string containing the site code identifier.</value>
        /// </summary>
        [Required]
        [ForeignKey("SiteId")]
        public string? SiteId { get; set; }

        /// <summary>
        /// Property <c>SiteCode</c> represents the reference navigation for the sites table.
        /// <value>A class containing the site codes.</value>
        /// </summary>
        [Required]
        public Site? Site { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatusId</c> represents the identifier for the work orders status table.
        /// <value>A string containing the work order status identifier.</value>
        /// </summary>
        [Required]
        [ForeignKey("WorkOrdersStatusId")]
        public string? WorkOrderStatusId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatus</c> represents the reference navigation for the work orders statuses table.
        /// <value>A class containing the work order status data model.</value>
        /// </summary>
        [Required]
        public WorkOrderStatus? WorkOrderStatus { get; set; }

        /// <summary>
        /// Property <c>WorkOrderHistories</c> represents the collection of work order histories.
        /// <value>An interface representing the contract for the collection of history of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrderHistory>? WorkOrderHistories { get; set; }

        /// <summary>
        /// Property <c>WorkOrderEvents</c> represents the collection of work order events.
        /// <value>An interface representing the contract for the collection of work order events. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrderEvent>? WorkOrderEvents { get; set; }
    }
}
