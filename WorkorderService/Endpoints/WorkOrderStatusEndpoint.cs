using DigitalTwin.WebAPI.Interfaces.WorkOrders;
using DigitalTwin.WebAPI.Models.WorkOrders;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace WorkOrderService.Endpoints
{
    /// <summary>
    /// Class <c>WorkOrderEndpoints</c> represents the Site Endpoint.
    /// </summary>
    public static class WorkOrderStatusEndpoint
    {
        /// <summary>
        /// Method <c>MapSiteEndpoints</c> maps the Site HTTP Endpoints by making use of route grouping.
        /// This is to auto-group endpoints in OpenAPI.
        /// </summary>
        /// <param name="routes">The interface representing the contract for the endpoint route builder.</param>
        public static void MapWorkOrderStatusEndpoints(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var workOrderStatusGroup = routes.MapGroup("/api/workorderstatus")
                                  .WithTags("WorkOrderStatuses");

            // GET: /api/workorderstatus
            workOrderStatusGroup.MapGet("/", async (IWorkOrderService service) =>
            {
                var workOrderStatuses = await service.GetAllAsync();
                return Results.Ok(workOrderStatuses);
            });

            // GET: /api/workorderstatus/{workOrderStatusId}
            workOrderStatusGroup.MapGet("/{workOrderStatusId:string}", async (string workOrderStatusId, IWorkOrderStatusService service) =>
            {
                var workOrderStatus = await service.GetByIdAsync(workOrderStatusId);
                return workOrderStatus is not null ? Results.Ok(workOrderStatus) : Results.NotFound();
            });

            // POST: /api/workorderstatus
            workOrderStatusGroup.MapPost("/", async (WorkOrderStatus workOrderStatus, IWorkOrderStatusService service) =>
            {
                var createdWorkOrderStatus = await service.CreateAsync(workOrderStatus);
                return TypedResults.Created($"/api/workorders/{createdWorkOrderStatus.WorkOrderStatusId}");
            });

            // PUT: /api/workorderstatus/{workOrderStatusId}
            workOrderStatusGroup.MapPut("/{workOrderStatusId:string}", async (string workOrderStatusId, WorkOrderStatus workOrderStatus, IWorkOrderStatusService service) =>
            {
                var updatedWorkOrderStatus = await service.UpdateAsync(workOrderStatusId, workOrderStatus);
                return updatedWorkOrderStatus ? Results.NoContent() : Results.NotFound();
            });

            // DELETE: /api/workorderstatus
            workOrderStatusGroup.MapDelete("/{workOrderStatusId:string}", async (string workOrderStatusId, IWorkOrderStatusService service) =>
            {
                var removedWorkOrderStatus = await service.DeleteAsync(workOrderStatusId);
                return removedWorkOrderStatus ? Results.NoContent() : Results.NotFound();
            });
        }
    }
}
