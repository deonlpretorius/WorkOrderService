using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>WorkOrderService.Models</c> contains the data models that represent the database tables for the application.
/// </summary>
namespace WorkOrderService.Models
{
    /// <summary>
    /// Class <c>SiteCode</c> represents the sites table.
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
        /// Property <c>SiteId</c> represents the identifier for the Sites table.
        /// <value>A string containing the site identifier.</value>
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
        /// Property <c>Code</c> represents the identification code of the site.
        /// <value>A string containing the site code. Default is Empty.</value>
        /// </summary>
        [Required]
        [StringLength(10)]
        public string? SiteCode {  get; set; }

        /// <summary>
        /// Property <c>LastModified</c> represents the last modification date and time of the site record.
        /// <value>A datetime containing the last modification date and time. Default is DateTime.Now</value>
        /// </summary>
        [Required]
        public DateTime LastModified { get; set; }
    }
}
