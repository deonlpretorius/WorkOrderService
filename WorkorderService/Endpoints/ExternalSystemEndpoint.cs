using DigitalTwin.WorkOrderService.Models;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.Endpoints
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
            externalSystemGroup.MapGet("/", GetExternalSystemsAsync)
            .WithName("GetAllExternalSystems");

            // GET: /api/externalsystem/{externalSystemId}
            externalSystemGroup.MapGet("/{externalSystemId:string}",  GetExternalSystemByIdAsync)
            .WithName("GetExternalSystemById");

            // GET: /api/externalsystem
            externalSystemGroup.MapGet("/", GetExternalSystemByCodeAsync)
            .WithName("GetExternalSystemByCode");

            // POST: /api/externalsystem
            externalSystemGroup.MapPost("/", CreateExternalSystemAsync)
            .WithName("CreateExternalSystem");

            // PUT: /api/site/{externalSystemId}
            externalSystemGroup.MapPut("/{externalSystemId:string}", UpdateExternalSystemAsync)
            .WithName("UpdateExternalSystem");

            // DELETE: /api/externalSystem
            externalSystemGroup.MapDelete("/{externalSystem:string}", DeleteExternalSystemAsync)
            .WithName("DeleteExternalSystem");
        }

        // This will help with unit testing.
        // Get All External Systems
        public static async Task<IResult> GetExternalSystemsAsync(IExternalSystemService service)
        {
            var externalSystems = await service.GetAllAsync();
            return TypedResults.Ok(externalSystems);
        }

        // Get External System By Id
        public static async Task<Results<Ok<ExternalSystem>, NotFound>> GetExternalSystemByIdAsync(string externalSystemId, IExternalSystemService service)
        {
            var externalSystems = await service.GetByIdAsync(externalSystemId);
            return externalSystems is not null ? TypedResults.Ok(externalSystems) : TypedResults.NotFound();
        }

        // Get External System By External System Code
        public static async Task<Results<Ok<ExternalSystem>, NotFound>> GetExternalSystemByCodeAsync(string externalSystemCode, IExternalSystemService service)
        {
            var externalSystem = await service.GetByCodeAsync(externalSystemCode);
            return TypedResults.Ok(externalSystem);
        }

        // Create External System
        public static async Task<IResult> CreateExternalSystemAsync(ExternalSystem externalSystem, IExternalSystemService service)
        {
            var createdExternalSystem = await service.CreateAsync(externalSystem);
            return TypedResults.Created($"/api/externalsystems/{createdExternalSystem.ExternalSystemId}");
        }

        // Update External System
        public static async Task<Results<NoContent, NotFound>> UpdateExternalSystemAsync(string externalSystemId, ExternalSystem externalSystem, IExternalSystemService service)
        {
            var updatedExternalSystem = await service.UpdateAsync(externalSystemId, externalSystem);
            return updatedExternalSystem ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        // Delete External Systems
        public static async Task<Results<NoContent, NotFound>> DeleteExternalSystemAsync(string externalSystemId, IExternalSystemService service)
        {
            var removedExternalSystem = await service.DeleteAsync(externalSystemId);
            return removedExternalSystem ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
