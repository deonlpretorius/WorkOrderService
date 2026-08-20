using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>WorkOrderService.Models</c> contains the Work Order data models that represent the database tables.
/// </summary>
namespace WorkOrderService.Models.WorkOrders
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
            Status = Enums.WorkOrderStatus.Pending;
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
        public string? StatusName { get; set; }

        /// <summary>
        /// Property <c>StatusDescription</c> represents the description of the work order status.
        /// <value>A string containing the work order status description. Default is Empty.</value>
        /// </summary>
        [StringLength(250)]
        public string? StatusDescription { get; set; }

        /// <summary>
        /// Property <c>Status</c> represents the work order status.
        /// <value>An enum containing the work order status. Default is Pending.</value>
        /// </summary>
        [Required]
        public Enums.WorkOrderStatus Status {  get; set; }

        /// <summary>
        /// Property <c>WorkOrders</c> represents the work orders.
        /// <value>An interface representing the contract for the collection of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrder>? WorkOrders { get; set; }

    }
}
