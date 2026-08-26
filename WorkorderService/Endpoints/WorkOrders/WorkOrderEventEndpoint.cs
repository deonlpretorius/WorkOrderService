using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;
using Microsoft.AspNetCore.Http.HttpResults;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Endpoints.WorkOrders</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Endpoints.WorkOrders
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
            workOrderEventGroup.MapGet("/", GetAllWorkOrderEventsAsync)
            .WithName("GetAllWorkOrderEvents");

            // GET: /api/workorderevents/{workordereventId}
            workOrderEventGroup.MapGet("/", GetWorkOrderEventsAsync)
            .WithName("GetWorkOrderEventById");

            // POST: /api/workorderevents
            workOrderEventGroup.MapPost("/", CreateWorkOrderEventAsync)
            .WithName("CreateWorkOrderEvent");
        }

        // This makes it easier for unit testing.
        // Get All Work Order Events.
        public static async Task<IResult> GetAllWorkOrderEventsAsync(IWorkOrderEventService service)
        {
            var workOrderEvents = await service.GetAllAsync();
            return TypedResults.Ok(workOrderEvents);
        }

        // Get Work Orders Events By Id
        public static async Task<Results<Ok<WorkOrderEvent>, NotFound>> GetWorkOrderEventsAsync(string workOrderEventId, IWorkOrderEventService service)
        {
            var workOrderEvent = await service.GetByIdAsync(workOrderEventId);
            return workOrderEvent is not null ? TypedResults.Ok(workOrderEvent) : TypedResults.NotFound();
        }

        // Create Work Order Event
        public static async Task<IResult> CreateWorkOrderEventAsync(WorkOrderEvent workOrderEvent, IWorkOrderEventService service)
        {
            var createdWorkOrderEvent = await service.CreateAsync(workOrderEvent);
            return TypedResults.Created($"/api/workordersevent/{createdWorkOrderEvent.SiteId}");
        }
    }
}
