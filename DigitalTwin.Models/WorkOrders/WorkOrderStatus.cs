using DigitalTwin.Enums.WorkOrders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>DigitalTwin.Models.WorkOrders</c> contains the Work Order data models that represent the database tables.
/// </summary>
namespace DigitalTwin.Models.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderStatus</c> represents the work orders status table.
    /// </summary>
    [Table("WorkOrderStatuses")]
    public class WorkOrderStatus
    {
        /// <summary>
        /// Constructor <c>WorkOrderStatus</c> is used to instantiate the Work Order Status data model.
        /// </summary>
        public WorkOrderStatus()
        {
            WorkOrderStatusId = Guid.NewGuid().ToString();
            Status = WorkOrderStatusType.Pending;
        }

        /// <summary>
        /// Property <c>WorkOrderStatusId</c> represents the identifier for the work order status.
        /// <value>A string containing the work order status identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? WorkOrderStatusId { get; set; }

        /// <summary>
        /// Property <c>StatusName</c> represents the name of the work order status.
        /// <value>A string containing the work order status name. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(20)]
        public string? WorkOrderStatusName { get; set; }

        /// <summary>
        /// Property <c>StatusDescription</c> represents the description of the work order status.
        /// <value>A string containing the work order status description. Default is Empty.</value>
        /// </summary>
        [StringLength(250)]
        public string? WorkOrderStatusDescription { get; set; }

        /// <summary>
        /// Property <c>Status</c> represents the work order status.
        /// <value>An enum containing the work order status. Default is Pending.</value>
        /// </summary>
        [Required]
        public WorkOrderStatusType Status {  get; set; }

        /// <summary>
        /// Property <c>WorkOrders</c> represents the work orders.
        /// <value>An interface representing the contract for the collection of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrder>? WorkOrders { get; set; }

        /// <summary>
        /// Property <c>WorkOrderHistories</c> represents the collection of work order histories.
        /// <value>An interface representing the contract for the collection of history of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrderHistory>? WorkOrderHistories { get; set; }

        /// <summary>
        /// Property <c>WorkOrderEvents</c> represents the collection of work order events.
        /// <value>An interface representing the contract for the collection of work order event data models.</value>
        /// </summary>
        public ICollection<WorkOrderEvent>? WorkOrderEvents { get; set; }

    }
}
