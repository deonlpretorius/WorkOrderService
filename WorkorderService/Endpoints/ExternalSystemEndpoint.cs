using WorkOrderService.Interfaces;

using WorkOrderService.Models;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Endpoints
{
    /// <summary>
    /// Class <c>WorkOrderEndpoints</c> represents the Site Endpoint.
    /// </summary>
    public static class ExternalSystemEndpoint
    {
        /// <summary>
        /// Method <c>MapSiteEndpoints</c> maps the Site HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapExternalSystemEndpoint(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var externalSystemGroup = routes.MapGroup("/api/externalsystem")
                                  .WithTags("ExternalSystems");

            // GET: /api/externalsystem
            externalSystemGroup.MapGet("/", async (IExternalSystemsService service) =>
            {
                var externalSystems = await service.GetAllAsync();
                return Results.Ok(externalSystems);
            });

            // GET: /api/externalsystem/{externalSystemId}
            externalSystemGroup.MapGet("/{externalSystemId:string}", async (string externalSystemId, IExternalSystemsService service) =>
            {
                var externalSystems = await service.GetByIdAsync(externalSystemId);
                return externalSystems is not null ? Results.Ok(externalSystems) : Results.NotFound();
            });

            // POST: /api/externalsystem
            externalSystemGroup.MapPost("/", async (ExternalSystem externalSystem, IExternalSystemsService service) =>
            {
                var createdExternalSystem = await service.CreateAsync(externalSystem);
                return TypedResults.Created($"/api/workorders/{createdExternalSystem.ExternalSystemId}");
            });

            // PUT: /api/site/{externalSystemId}
            externalSystemGroup.MapPut("/{externalSystemId:string}", async (string externalSystemId, ExternalSystem externalSystem, IExternalSystemsService service) =>
            {
                var updatedExternalSystem = await service.UpdateAsync(externalSystemId, externalSystem);
                return updatedExternalSystem ? Results.NoContent() : Results.NotFound();
            });

            // DELETE: /api/externalSystem
            externalSystemGroup.MapDelete("/{externalSystem:string}", async (string externalSystem, IExternalSystemsService service) =>
            {
                var removedExternalSystem = await service.DeleteAsync(externalSystem);
                return removedExternalSystem ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
