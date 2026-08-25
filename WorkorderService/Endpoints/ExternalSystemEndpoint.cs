using DigitalTwin.WebAPI.Interfaces;
using DigitalTwin.WebAPI.Models;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.Endpoints
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
            var externalSystemGroup = routes.MapGroup("/api/externalsystems")
                                  .WithTags("ExternalSystems");

            // GET: /api/externalsystem
            externalSystemGroup.MapGet("/", async (IExternalSystemService service) =>
            {
                var externalSystems = await service.GetAllAsync();
                return Results.Ok(externalSystems);
            })
            .WithName("GetAllExternalSystems");

            // GET: /api/externalsystem/{externalSystemId}
            externalSystemGroup.MapGet("/{externalSystemId:string}", async (string externalSystemId, IExternalSystemService service) =>
            {
                var externalSystems = await service.GetByIdAsync(externalSystemId);
                return externalSystems is not null ? Results.Ok(externalSystems) : Results.NotFound();
            })
            .WithName("GetExternalSystemById");

            // GET: /api/externalsystem
            externalSystemGroup.MapGet("/", async (string externalSystemCode, IExternalSystemService service) =>
            {
                var externalSystem = await service.GetByCodeAsync(externalSystemCode);
                return Results.Ok(externalSystem);
            })
            .WithName("GetExternalSystemByCode");

            // POST: /api/externalsystem
            externalSystemGroup.MapPost("/", async (ExternalSystem externalSystem, IExternalSystemService service) =>
            {
                var createdExternalSystem = await service.CreateAsync(externalSystem);
                return TypedResults.Created($"/api/workorders/{createdExternalSystem.ExternalSystemId}");
            })
            .WithName("CreateExternalSystem");

            // PUT: /api/site/{externalSystemId}
            externalSystemGroup.MapPut("/{externalSystemId:string}", async (string externalSystemId, ExternalSystem externalSystem, IExternalSystemService service) =>
            {
                var updatedExternalSystem = await service.UpdateAsync(externalSystemId, externalSystem);
                return updatedExternalSystem ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateExternalSystem");

            // DELETE: /api/externalSystem
            externalSystemGroup.MapDelete("/{externalSystem:string}", async (string externalSystem, IExternalSystemService service) =>
            {
                var removedExternalSystem = await service.DeleteAsync(externalSystem);
                return removedExternalSystem ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteExternalSystem");
        }
    }
}
