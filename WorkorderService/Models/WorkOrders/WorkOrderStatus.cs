using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>WorkOrderService.Models.WorkOrders</c> contains the Work Order data models for the application.
/// </summary>
namespace WorkOrderService.Models.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderStatus</c> represents the Work Order Status table.
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
        /// Property <c>WorkOrderStatusId</c> represents the identifier for the work order status table.
        /// <value>A string containing the work order status identifier. Default is NewGuid.</value>
        /// </summary>
        [Required]
        [Key]
        public string WorkOrderStatusId { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatusName</c> represents the name of the work order status.
        /// <value>A string containing the work order status name. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? WorkOrderStatusName { get; set; }

        /// <summary>
        /// Property <c>WorkOrderStatusDescription</c> represents the description of the work order status.
        /// <value>A string containing the work order description. Default is Empty.</value>
        /// </summary>
        [StringLength(250)]
        public string? WorkOrderStatusDescription { get; set; }

        /// <summary>
        /// Property <c>Status</c> represents the status of a work order.
        /// <value>An enum containing the work order status. Default is Pending.</value>
        /// </summary>
        [Required]
        public Enums.WorkOrderStatus? Status { get; set; }
    }
}
