using WorkOrderService.Interfaces;
using WorkOrderService.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Endpoints
{
    /// <summary>
    /// Class <c>WorkOrderEndpoints</c> represents the Work Orders Endpoint.
    /// </summary>
    public static class WorkOrderEndpoints
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
            workOrderGroup.MapGet("/", async (IWorkOrdersService workOrdersService) =>
            {
                var workOrders = await workOrdersService.GetAllAsync();
                return Results.Ok(workOrders);
            });

            // GET: /api/workorders/{workOrderId}
            workOrderGroup.MapGet("/{workOrderId:string}", async (string workOrderId, IWorkOrdersService service) =>
            {
                var workOrder = await service.GetByIdAsync(workOrderId);
                return workOrder is not null ? Results.Ok(workOrder) : Results.NotFound();
            });

            // POST: /api/workorders
            workOrderGroup.MapPost("/", async (WorkOrder workOrder, IWorkOrdersService workOrdersService) =>
            {
                var createdWorkOrder = await workOrdersService.CreateAsync(workOrder);
                return TypedResults.Created($"/api/workorders/{createdWorkOrder.WorkOrderId}");
            });

            // PUT: /api/workorders/{workOrderId}
            workOrderGroup.MapPut("/{workOrderId:string}", async (string workOrderId, WorkOrder workOrder, IWorkOrdersService workOrdersService) =>
            {
                var updatedWorkOrder = await workOrdersService.UpdateAsync(workOrderId, workOrder);
                return updatedWorkOrder ? Results.NoContent() : Results.NotFound();
            });

            // DELETE: /api/workorders
            workOrderGroup.MapDelete("/{workOrderId:string}", async (string workOrderId, IWorkOrdersService workOrdersService) =>
            {
                var removedWorkOrder = await workOrdersService.DeleteAsync(workOrderId);
                return removedWorkOrder ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
