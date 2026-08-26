using DigitalTwin.Models.WorkOrders;
using DigitalTwin.WebAPI.Interfaces.WorkOrders;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Endpoints.WorkOrders</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Endpoints.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderEventEndpoint</c> represents the Work Order Event Endpoint.
    /// </summary>
    public static class WorkOrderEventEndpoint
    {
        /// <summary>
        /// Method <c>MapWorkOrderEventEndpoints</c> maps the Work Order Event HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapWorkOrderEventEndpoints(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Group for a cleaner API.
            var workOrderEventGroup = routes.MapGroup("/api/workorderevents")
                                            .WithTags("WorkOrderEvents");

            // GET: /api/workorderevents
            workOrderEventGroup.MapGet("/", async (IWorkOrderEventService service) =>
            {
                var workOrderEvents = await service.GetAllAsync();
                return Results.Ok(workOrderEvents);
            })
            .WithName("GetAllWorkOrderEvents");

            // GET: /api/workorderevents/{workordereventId}
            workOrderEventGroup.MapGet("/", async (string workOrderEventId, IWorkOrderEventService service) =>
            {
                var workOrderEvent = await service.GetByIdAsync(workOrderEventId);
                return workOrderEvent is not null ? Results.Ok(workOrderEvent) : Results.NotFound();
            })
            .WithName("GetWorkOrderEventById");

            // POST: /api/workorderevents
            workOrderEventGroup.MapPost("/", async (WorkOrderEvent workOrderEvent, IWorkOrderEventService service) => 
            {
                var createdWorkOrderEvent = await service.CreateAsync(workOrderEvent);
                return TypedResults.Created($"/api/workorders/{createdWorkOrderEvent.SiteId}");
            })
            .WithName("CreateWorkOrderEvent");
        }
    }
}
