using DigitalTwin.Enums.WorkOrders;
using DigitalTwin.Models.WorkOrders;
using DigitalTwin.WebAPI.Interfaces.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Endpoints.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrdeHistoryrEndpoints</c> represents the Site Endpoint.
    /// </summary>
    public static class WorkOrderHistoryEndpoint
    {
        /// <summary>
        /// Method <c>MapWorkOrderHistoryEndpoint</c> maps the Work Order  HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapWorkOrderHistoryEndpoint(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var workOrderHistoryGroup = routes.MapGroup("/api/workorderhistories")
                                              .WithTags("WorkOrderHistory");

            // GET: /api/workorderhistories
            workOrderHistoryGroup.MapGet("/", async (IWorkOrderHistoryService service) =>
            {
                var workOrderHistories = await service.GetAllAsync();
                return TypedResults.Ok(workOrderHistories);
            })
            .WithName("GetAllWorkOrderHistories");

            // GET: /api/workorderhistories/{workOrderHistoryId}
            workOrderHistoryGroup.MapGet("/{workOrderHistoryId:string}", async (string workOrderHistoryId, IWorkOrderHistoryService service) =>
            {
                var workOrderHistory = await service.GetByIdAsync(workOrderHistoryId);
                return workOrderHistory is not null ? Results.Ok(workOrderHistory) : Results.NotFound();
            })
            .WithName("GetWorkOrderHistoryById");

            // GET: /api/workorderhistories/
            workOrderHistoryGroup.MapGet("/", async (WorkOrderStatusType status, IWorkOrderHistoryService service) =>
            {
                var workOrderHistories = await service.GetByStatusAsync(status);
                return workOrderHistories is not null ? Results.Ok(workOrderHistories) : Results.NotFound();
            })
            .WithName("GetWorkOrderHistoryByStatus");

            // GET: /api/workorderhistories
            workOrderHistoryGroup.MapGet("/", async (string workOrderId, IWorkOrderHistoryService service) =>
            {
                var workOrderHistories = await service.GetByWorkOrderIdAsync(workOrderId);
                return workOrderHistories is not null ? Results.Ok(workOrderHistories) : Results.NotFound();
            })
            .WithName("GetWorkOrderHistoryByWorkOrderId");

            // GET: /api/workorderhistories
            workOrderHistoryGroup.MapGet("/", async (string workOrderId, WorkOrderStatusType status, IWorkOrderHistoryService service) =>
            {
                var workOrderHistories = await service.GetByWorkOrderIdAndStatusAsync(workOrderId, status);
                return workOrderHistories is not null ? Results.Ok(workOrderHistories) : Results.NotFound();
            })
            .WithName("GetWorkOrderHistoryByWorkOrderIdAndStatus");

            // POST: /api/workorderhistories
            workOrderHistoryGroup.MapPost("/", async (WorkOrderHistory workOrderHistory, IWorkOrderHistoryService service) =>
            {
                var createdWorkOrderHistory = await service.CreateAsync(workOrderHistory);
                return TypedResults.Created($"/api/workorderhistory/{createdWorkOrderHistory.WorkOrderHistoryId}");
            })
            .WithName("CreateWorkOrderHistory");
        }
    }
}
