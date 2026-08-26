using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;
using Microsoft.AspNetCore.Http.HttpResults;

/// <summary>
/// Namespace <c>DigitalTwin.WebAPI.Endpoints.WorkOrders</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Endpoints.WorkOrders
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
            var workOrderStatusGroup = routes.MapGroup("/api/workorderstatuses")
                                  .WithTags("WorkOrderStatuses");

            // GET: /api/workorderstatus
            workOrderStatusGroup.MapGet("/", GetAllWorkOrderStatusesAsync)
            .WithName("GetAllWorkOrderStatuses");

            // GET: /api/workorderstatus/{workOrderStatusId}
            workOrderStatusGroup.MapGet("/{workOrderStatusId:string}", GetWorkOrderStatusByIdAsync)
            .WithName("GetWorkOrderStatusById");

            // POST: /api/workorderstatus
            workOrderStatusGroup.MapPost("/", CreateWorkOrderStatusAsync)
            .WithName("CreateWorkOrderStatus");

            // PUT: /api/workorderstatus/{workOrderStatusId}
            workOrderStatusGroup.MapPut("/{workOrderStatusId:string}", UpdateWorkOrderStatusAsync)
            .WithName("UpdateWorkOrderStatus");

            // DELETE: /api/workorderstatus
            workOrderStatusGroup.MapDelete("/{workOrderStatusId:string}", DeleteWorkOrderStatusAsync)
            .WithName("DeleteWorkOrderStatus");
        }

        // This makes it easier for unit testing.
        // Get All Work Order Statuses
        public static async Task<IResult> GetAllWorkOrderStatusesAsync(IWorkOrderStatusService service)
        {
            var workOrderStatuses = await service.GetAllAsync();
            return TypedResults.Ok(workOrderStatuses);
        }

        // Get Work Order Status By Id
        public static async Task<Results<Ok<WorkOrderStatus>, NotFound>> GetWorkOrderStatusByIdAsync(string workOrderStatusId, IWorkOrderStatusService service)
        {
            var workOrderStatus = await service.GetByIdAsync(workOrderStatusId);
            return workOrderStatus is not null ? TypedResults.Ok(workOrderStatus) : TypedResults.NotFound();
        }

        // Create Work Order Status
        public static async Task<IResult> CreateWorkOrderStatusAsync(WorkOrderStatus workOrderStatus, IWorkOrderStatusService service)
        {
            var createdWorkOrderStatus = await service.CreateAsync(workOrderStatus);
            return TypedResults.Created($"/api/workorders/{createdWorkOrderStatus.WorkOrderStatusId}");
        }

        // Update Work Order Status
        public static async Task<Results<NoContent, NotFound>> UpdateWorkOrderStatusAsync(string workOrderStatusId, WorkOrderStatus workOrderStatus, IWorkOrderStatusService service)
        {
            var updatedWorkOrderStatus = await service.UpdateAsync(workOrderStatusId, workOrderStatus);
            return updatedWorkOrderStatus ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        // Delete Work Order Status
        public static async Task<Results<NoContent, NotFound>> DeleteWorkOrderStatusAsync(string workOrderStatusId, IWorkOrderStatusService service)
        {
            var removedWorkOrderStatus = await service.DeleteAsync(workOrderStatusId);
            return removedWorkOrderStatus ? TypedResults.NoContent() : TypedResults.NotFound();
        }
    }
}
