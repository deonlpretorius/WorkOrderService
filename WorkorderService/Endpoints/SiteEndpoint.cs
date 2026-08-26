using DigitalTwin.WorkOrderService.Models;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Endpoints
{
    /// <summary>
    /// Class <c>SiteEndpoint</c> represents the Site Endpoint.
    /// </summary>
    public static class SiteEndpoint
    {
        /// <summary>
        /// Method <c>MapSiteEndpoints</c> maps the Site HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapSiteEndpoints(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var siteGroup = routes.MapGroup("/api/sites")
                                  .WithTags("Sites");

            // GET: /api/site
            siteGroup.MapGet("/", GetAllSitesAsync)
            .WithName("GetAllSites");

            // GET: /api/site/{siteId}
            siteGroup.MapGet("/{siteId:string}", GetSiteByIdAsync)
            .WithName("GetSiteById");

            // GET: /api/site/
            siteGroup.MapGet("/", GetSiteBySiteCodeAsync)
            .WithName("GetSiteBySiteCode");

            // POST: /api/site
            siteGroup.MapPost("/", CreateSiteAsync)
            .WithName("CreateSite");

            // PUT: /api/site/{siteId}
            siteGroup.MapPut("/{siteId:string}", UpdateSiteAsync)
            .WithName("UpdateSite");

            // DELETE: /api/site
            siteGroup.MapDelete("/{siteId:string}", DeleteSiteAsync)
            .WithName("DeleteSite");
        }

        // This will help with the unit testing.
        // Get All Sites
        public static async Task<IResult> GetAllSitesAsync(ISiteService service)
        {
            var sites = await service.GetAllAsync();
            return TypedResults.Ok(sites);
        }

        // Get Site By Id
        public static async Task<Results<Ok<Site>, NotFound>> GetSiteByIdAsync(string siteId, ISiteService service)
        {
            var site = await service.GetByIdAsync(siteId);
            return site is not null ? TypedResults.Ok(site) : TypedResults.NotFound();
        }

        // Get Site By Site Code
        public static async Task<Results<Ok<Site>, NotFound>> GetSiteBySiteCodeAsync(string siteCode, ISiteService service)
        {
             var site = await service.GetBySiteCodeAsync(siteCode);
            return site is not null ? TypedResults.Ok(site) : TypedResults.NotFound();
        }

        // Create Site
        public static async Task<IResult> CreateSiteAsync(Site site, ISiteService service)
        {
            var createdSite = await service.CreateAsync(site);
            return TypedResults.Created($"/api/sites/{createdSite.SiteId}");
        }

        // Updated Site
        public static async Task<Results<NoContent, NotFound>> UpdateSiteAsync(string siteId, Site site, ISiteService service)
        {
            var updatedSite = await service.UpdateAsync(siteId, site);
            return updatedSite ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        // Delete Site
        public static async Task<Results<NoContent, NotFound>> DeleteSiteAsync(string siteId, ISiteService service)
        {
            var removedSite = await service.DeleteAsync(siteId);
            return removedSite ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
