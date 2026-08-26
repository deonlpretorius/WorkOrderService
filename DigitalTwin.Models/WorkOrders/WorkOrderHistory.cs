using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>DigitalTwin.Models.WorkOrders</c> contains the data models that represent the database tables.
/// </summary>
namespace DigitalTwin.Models.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrdersHistory</c> represents the work orders history table.
    /// </summary>
    [Table("WorkOrderHistories")]
    public class WorkOrderHistory
    {
        /// <summary>
        /// Constructor <c>WorkOrderHistory</c> is used to instantiate the work order history data model.
        /// </summary>
        public WorkOrderHistory()
        {
            WorkOrderHistoryId = Guid.NewGuid().ToString();
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Property <c>WorkOrderHistoryId</c> represents the identifier for the work order histories table.
        /// <value>A string containing the work order history identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? WorkOrderHistoryId { get; set; }

        /// <summary>
        /// Property <c>Status</c> represents the identifier of the work order status table.
        /// <value>An enum containing the work order status.</value>
        /// </summary>
        [Required]
        public string? WorkOrderStatusId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatus</c> represents the reference navigation for the work order status table.
        /// <value>A class containing the work order status data model.</value>
        /// </summary>
        [Required]
        public WorkOrderStatus? WorkOrderStatus { get; set; }

        /// <summary>
        /// Property <c>UpdatedAt</c> represents the update date and time of the work order status.
        /// <value>A datetime containing the work order status update. Default is DateTime.Now</value>
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Property <c>WorkOrderId</c> represents the identifier of the work orders table.
        /// <value>A string containing the work order identififier.</value>
        /// </summary>
        [Required]
        public string? WorkOrderId { get; set; }

        /// <summary>
        /// Property <c>WorkOrder</c> represents the reference navigation for the work orders table.
        /// <value>A class containing the work orders data model.</value>
        /// </summary>
        [Required]
        public WorkOrder? WorkOrder { get; set; }
    }
}
