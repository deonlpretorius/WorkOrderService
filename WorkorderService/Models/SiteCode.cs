using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Namespace <c>WorkOrderService.Models</c> contains the data models that represent the database tables.
/// </summary>
namespace WorkOrderService.Models
{
    /// <summary>
    /// Class <c>SiteCode</c> represents the site codes table.
    /// </summary>
    [Table("SiteCodes")]
    public class SiteCode
    {
        /// <summary>
        /// Constructor <c>SiteCode</c> is used to instantiate the SiteCodes data model.
        /// </summary>
        public SiteCode()
        {
            SiteCodeId = Guid.NewGuid().ToString();
            LastModified = DateTime.Now;
        }

        /// <summary>
        /// Property <c>SiteCodeId</c> represents the site code identifier for the table.
        /// <value>A string containing the site code identifier.</value>
        /// </summary>
        [Required]
        [Key]
        public string? SiteCodeId { get; set; }

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
        public string? Code {  get; set; }

        [Required]
        public DateTime LastModified { get; set; }
    }
}
