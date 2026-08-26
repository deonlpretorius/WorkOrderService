using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>DigitalTwin.Models.WorkOrders</c> contains the data models that represent the database tables.
/// </summary>
namespace DigitalTwin.Models.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderEvent</c> represents the work order events table.
    /// </summary>
    [Table("WorkOrderEvents")]
    public class WorkOrderEvent
    {
        /// <summary>
        /// Constructor <c>WorkOrderEvent</c> is used to instantiate the work order events data model.
        /// </summary>
        public WorkOrderEvent()
        {
            WorkOrderEventId = Guid.NewGuid().ToString();
            OccurredAt = DateTime.Now;
        }

        /// <summary>
        /// Property <c>WorkOrderEventId</c> represents the identifier for the work order event.
        /// <value>A string containing the work order event identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? WorkOrderEventId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderExternalId</c> represents the identifier for the work order in an external system.
        /// <value>A string containing the work order external system identifier. Default is Empty.</value>
        /// </summary>
        [StringLength(15)]
        public string? WorkOrderExternalId { get; set; }

        /// <summary>
        /// Property <c>OccurredAt</c> represents when the event has occurred.
        /// <value>A datetime containing the work order event update occurance. Default is DateTime.Now</value>
        /// </summary>
        [Required]
        public DateTime OccurredAt { get; set; }

        /// <summary>
        /// Property <c>Details</c> represents the extra details for the work order event.
        /// <value>A string containing the extra details for the work order event. Default is Empty.</value>
        /// </summary>
        public string? Details { get; set; }

        /// <summary>
        /// Property <c>WorkOrderExternalId</c> represents the identifier for the work order in an external system.
        /// <value>A string containing the work order external identifier. Default is Empty.</value>
        /// </summary>
        [ForeignKey("ExternalSystemId")]
        public string? ExternalSystemId { get; set; }

        /// <summary>
        /// Property <c>ExternalSystem</c> represents the reference navigation for the external systems table.
        /// This is used for system integration purposes.
        /// <value>A class containing the external systems data model. Default is Null.</value>
        /// </summary>
        public ExternalSystem? ExternalSystem { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatusId</c> represents the status of the work order (latest status update).
        /// This is used for system integration purposes.
        /// <value>An enum containing the work order status.</value>
        /// </summary>
        [Required]
        [ForeignKey("WorkOrderStatusId")]
        public string? WorkOrderStatusId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatus</c> represents the reference navigation for the work order statuses table.
        /// <value>A class containing the work order status data model.</value>
        /// </summary>
        [Required]
        public WorkOrderStatus? WorkOrderStatus { get; set; }

        /// <summary>
        /// Property <c>SiteId</c> represents the identifier for the site codes table.
        /// <value>A string containing the site codes identifier.</value>
        /// </summary>
        [Required]
        [ForeignKey("SiteId")]
        public string? SiteId { get; set; }

        /// <summary>
        /// Property <c>Site</c> represents the reference navigation for the site codes table.
        /// <value>A class containing the site code data model.</value>
        /// </summary>
        [Required]
        public Site? Site { get; set; }

        /// <summary>
        /// Property <c>WorkOrderId</c> represents the identifier for the work order related to the event.
        /// <value>A string containing the work order identifier.</value>
        /// </summary>
        [Required]
        [ForeignKey("WorkOrderId")]
        public string? WorkOrderId { get; set; }

        /// <summary>
        /// Property <c>WorkOrder</c> represents the reference navigation for the work orders table.
        /// <value>A class containing the work orders data model.</value>
        /// </summary>
        [Required]
        public WorkOrder? WorkOrder { get; set; }
    }
}
