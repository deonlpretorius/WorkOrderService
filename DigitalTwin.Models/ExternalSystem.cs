using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalTwin.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.Models</c> contains the data models that represent the database tables.
/// </summary>
namespace DigitalTwin.Models
{
    /// <summary>
    /// Class <c>ExternalSystem</c> represetns the external systems table.
    /// </summary>
    [Table("ExternalSystems")]
    public class ExternalSystem
    {
        /// <summary>
        /// Constructor <c>ExternalSystem</c> is used to instantiate the External System data model.
        /// </summary>
        public ExternalSystem()
        {
            ExternalSystemId = Guid.NewGuid().ToString();
            LastModified = DateTime.Now;
        }

        /// <summary>
        /// Property <c>ExternalSystemId</c> represents the identifier for the external systems table.
        /// <value>A string containing the external systems identifier. Default is Empty.</value>
        /// </summary>
        [Required]
        [Key]
        public string ExternalSystemId { get; set; }

        /// <summary>
        /// Property <c>ExternalSystemName</c> represents the name of the external system.
        /// <value>A string containing the external system name. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? ExternalSystemName { get; set; }

        /// <summary>
        /// Property <c>ExternalSystemDescription</c> represents the description of the external system.
        /// <value>A string containing the external system description. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(250)]
        public string? ExternalSystemDescription { get; set; }

        /// <summary>
        /// Property <c>ExternalSystemCode</c> represents the code for the external system.
        /// <value>A string containing the external system code. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(15)]
        public string? ExternalSystemCode { get; set; }

        /// <summary>
        /// Property <c>LastModified</c> represents the last modification date and time for the external systems table record.
        /// <value>A datetime containing the last modification date and time. Default is DateTime.Now.</value>
        /// </summary>
        [Required]
        public DateTime? LastModified {  get; set; }

        /// <summary>
        /// Property <c>WorkOrders</c> represents the collection of work orders.
        /// <value>An interface representing the contract for the collection of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrder>? WorkOrders { get; set; }

        /// <summary>
        /// Property <c>WorkOrderEvents</c> represents the collection of work order events.
        /// <value>An interface representing the contract for the collection of work order event data models.</value>
        /// </summary>
        public ICollection<WorkOrderEvent>? WorkOrderEvents { get; set; }
    }
}
