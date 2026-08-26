using DigitalTwin.Enums.WorkOrders;
using DigitalTwin.Models.WorkOrders;
using DigitalTwin.WebAPI.Interfaces.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WebAPI.Endpoints.WorkOrders
{
    /// <summary>
    /// Class <c>WorkOrderEndpoints</c> represents the Work Orders Endpoint.
    /// </summary>
    public static class WorkOrderEndpoint
    {
        /// <summary>
        /// Method <c>MapWorkOrderEndpoints</c> maps the Work Order HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapWorkOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var workOrderGroup = routes.MapGroup("/api/workorders")
                                       .WithTags("WorkOrders");

            // GET: /api/workorders
            workOrderGroup.MapGet("/", async (IWorkOrderService workOrdersService) =>
            {
                var workOrders = await workOrdersService.GetAllAsync();
                return Results.Ok(workOrders);
            })
            .WithName("GetAllWorkOrders");

            // GET: /api/workorders/{workOrderId}
            workOrderGroup.MapGet("/{workOrderId:string}", async (string workOrderId, IWorkOrderService service) =>
            {
                var workOrder = await service.GetByIdAsync(workOrderId);
                return workOrder is not null ? Results.Ok(workOrder) : Results.NotFound();
            })
            .WithName("GetWorkOrderById");

            // GET: /api/workorders/{status}?pageNumber=2&pageSize=50
            workOrderGroup.MapGet("/", async (WorkOrderStatusType status, int pageNumber, int pageSize, IWorkOrderService service) =>
            {
                var workOrders = await service.GetByStatusAsync(status, pageNumber, pageSize);
                return TypedResults.Ok(workOrders);

            })
            .WithName("GetWorkOrderByStatus");

            // POST: /api/workorders
            workOrderGroup.MapPost("/", async (WorkOrder workOrder, IWorkOrderService workOrdersService) =>
            {
                var createdWorkOrder = await workOrdersService.CreateAsync(workOrder);
                return TypedResults.Created($"/api/workorders/{createdWorkOrder.WorkOrderId}");
            })
            .WithName("CreateWorkOrder");

            // PUT: /api/workorders/{workOrderId}
            workOrderGroup.MapPut("/{workOrderId:string}", async (string workOrderId, WorkOrder workOrder, IWorkOrderService workOrdersService) =>
            {
                var updatedWorkOrder = await workOrdersService.UpdateAsync(workOrderId, workOrder);
                return updatedWorkOrder ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateWorkOrder");

            // PUT: /api/workorders/{workOrderId}?status=Completed
            workOrderGroup.MapPut("/{workOrderId:string}", async (string workOrderId, WorkOrderStatusType status, IWorkOrderService service) =>
            {
                var updatedWorkOrder = await service.UpdateWorkOrderStatusAsync(workOrderId, status);
                return updatedWorkOrder ? Results.NoContent() : Results.NotFound();
            })
            .WithName("UpdateWorkOrderStatus");

            // DELETE: /api/workorders
            workOrderGroup.MapDelete("/{workOrderId:string}", async (string workOrderId, IWorkOrderService workOrdersService) =>
            {
                var removedWorkOrder = await workOrdersService.DeleteAsync(workOrderId);
                return removedWorkOrder ? Results.NoContent() : Results.NotFound();
            })
            .WithName("DeleteWorkOrder");
        }
    }
}
