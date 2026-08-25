using DigitalTwin.WebAPI.Interfaces;
using DigitalTwin.WebAPI.Models;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Endpoints
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
            var siteGroup = routes.MapGroup("/api/site")
                                  .WithTags("Sites");

            // GET: /api/site
            siteGroup.MapGet("/", async (ISiteService service) =>
            {
                var sites = await service.GetAllAsync();
                return Results.Ok(sites);
            })
            .WithName("GetAllSites"); 

            // GET: /api/site/{siteId}
            siteGroup.MapGet("/{siteId:string}", async (string siteId, ISiteService service) =>
            {
                var site = await service.GetByIdAsync(siteId);
                return site is not null ? Results.Ok(site) : Results.NotFound();
            })
            .WithName("GetSiteById");

            // GET: /api/site/
            siteGroup.MapGet("/", async (string siteCode, ISiteService service) =>
            {
                var site = await service.GetBySiteCodeAsync(siteCode);
                return Results.Ok(site);
            })
            .WithName("GetSiteBySiteCode");

            // POST: /api/site
            siteGroup.MapPost("/", async (Site site, ISiteService service) =>
            {
                var createdSite = await service.CreateAsync(site);
                return TypedResults.Created($"/api/workorders/{createdSite.SiteId}");
            })
            .WithName("CreateSite");

            // PUT: /api/site/{siteId}
            siteGroup.MapPut("/{siteId:string}", async (string siteId, Site site, ISiteService service) =>
            {
                var updatedSite = await service.UpdateAsync(siteId, site);
                return updatedSite ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateSite");

            // DELETE: /api/site
            siteGroup.MapDelete("/{siteId:string}", async (string siteId, ISiteService service) =>
            {
                var removedSite = await service.DeleteAsync(siteId);
                return removedSite ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteSite");
        }
    }
}
