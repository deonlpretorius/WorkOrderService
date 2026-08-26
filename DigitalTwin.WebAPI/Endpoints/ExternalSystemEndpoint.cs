using DigitalTwin.WebAPI.Interfaces;
using DigitalTwin.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

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
            externalSystemGroup.MapGet("/",  GetAllExternalSystems)
            .WithName("GetAllExternalSystems");

            // GET: /api/externalsystem/{externalSystemId}
            externalSystemGroup.MapGet("/{externalSystemId:string}", GetExternalSystemById)
            .WithName("GetExternalSystemById");

            // GET: /api/externalsystem
            externalSystemGroup.MapGet("/", GetExternalSystemByCode)
            .WithName("GetExternalSystemByCode");

            // POST: /api/externalsystem
            externalSystemGroup.MapPost("/", CreateExternalSystem)
            .WithName("CreateExternalSystem");

            // PUT: /api/site/{externalSystemId}
            externalSystemGroup.MapPut("/{externalSystemId:string}", UpdateExternalSystem)
            .WithName("UpdateExternalSystem");

            // DELETE: /api/externalSystem
            externalSystemGroup.MapDelete("/{externalSystem:string}", DeleteExternalSystem)
            .WithName("DeleteExternalSystem");
        }

        // Named static handler method that can be targeted easily by Unit Tests.
        public static async Task<IResult> GetAllExternalSystems(IExternalSystemService service)
        {
            var externalSystems = await service.GetAllAsync();
            return Results.Ok(externalSystems);
        }

        public static async Task<Results<Ok<ExternalSystem>, NotFound>> GetExternalSystemById(string externalSystemId, IExternalSystemService service)
        {
            var externalSystems = await service.GetByIdAsync(externalSystemId);
            return externalSystems is not null ? TypedResults.Ok(externalSystems) : TypedResults.NotFound();
        }

        public static async Task<Results<Ok<ExternalSystem>, NotFound>> GetExternalSystemByCode(string externalSystemCode, IExternalSystemService service)
        {
            var externalSystem = await service.GetByCodeAsync(externalSystemCode);
            if (externalSystem is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(externalSystem);
        }

        public static async Task<IResult> CreateExternalSystem(ExternalSystem externalSystem, IExternalSystemService service)
        {
            var createdExternalSystem = await service.CreateAsync(externalSystem);
            return TypedResults.Created($"/api/workorders/{createdExternalSystem.ExternalSystemId}");
        }

        public static async Task<Results<NoContent, NotFound>> UpdateExternalSystem(string externalSystemId, ExternalSystem externalSystem, IExternalSystemService service)
        {
            var updatedExternalSystem = await service.UpdateAsync(externalSystemId, externalSystem);
            return updatedExternalSystem ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        public static async Task<Results<NoContent, NotFound>> DeleteExternalSystem(string externalSystem, IExternalSystemService service)
        {
            var removedExternalSystem = await service.DeleteAsync(externalSystem);
            return removedExternalSystem ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
