using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DigitalTwin.WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.Models</c> contains the data models that represent the database tables for the application.
/// </summary>
namespace DigitalTwin.WorkOrderService.Models
{
    /// <summary>
    /// Class <c>SiteCode</c> represents the Sites table.
    /// </summary>
    [Table("Sites")]
    public class Site
    {
        /// <summary>
        /// Constructor <c>Site</c> is used to instantiate the Site data model.
        /// </summary>
        public Site()
        {
            SiteId = Guid.NewGuid().ToString();
            LastModified = DateTime.Now;
        }

        /// <summary>
        /// Property <c>SiteId</c> represents the identifier for the sites table.
        /// <value>A string containing the site code identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? SiteId { get; set; }

        /// <summary>
        /// Property <c>SiteName</c> represents the name of the site.
        /// <value>A string containing the site name. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(50)]
        public string? SiteName { get; set; }

        /// <summary>
        /// Property <c>SiteDescription</c> represents the description of the site.
        /// <value>A string containing the site description. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(250)]
        public string? SiteDescription { get; set; }

        /// <summary>
        /// Property <c>SiteCode</c> represents the identification code of a site.
        /// <value>A string containing the site code. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(10)]
        public string? SiteCode {  get; set; }

        /// <summary>
        /// Property <c>LastModified</c> represents the last modification date and time for the sites table record.
        /// <value>A datetime containing the last modification date and time. Default is DateTime.Now.</value>
        /// </summary>
        [Required]
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Property <c>WorkOrders</c> represents the collection of work orders.
        /// <value>An interface representing the contract for the collection of work orders. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrder>? WorkOrders { get; set; }

        /// <summary>
        /// Property <c>WorkOrderEvents</c> represents the collection of work order events.
        /// <value>An interface representing the contract for the collection of work order events. Default is Null.</value>
        /// </summary>
        public ICollection<WorkOrderEvent>? WorkOrderEvents { get; set; }
    }
}
