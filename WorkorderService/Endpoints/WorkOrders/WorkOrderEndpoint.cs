using DigitalTwin.WorkOrderService.Enums.WorkOrders;
using DigitalTwin.WorkOrderService.Models.WorkOrders;
using DigitalTwin.WorkOrderService.WebAPI.Interfaces.WorkOrders;
using Microsoft.AspNetCore.Http.HttpResults;

/// <summary>
/// Namespace <c>WorkOrderService.Endpoints</c> contains the HTTP Endpoints and Route Groupings for the Work Order Service application.
/// </summary>
namespace DigitalTwin.WorkOrderService.WebAPI.Endpoints.WorkOrders
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
            workOrderGroup.MapGet("/", GetAllWorkOrdersAsync)
            .WithName("GetAllWorkOrders");

            // GET: /api/workorders/{workOrderId}
            workOrderGroup.MapGet("/{workOrderId:string}", GetWorkOrdersById)
            .WithName("GetWorkOrderById");

            // GET: /api/workorders/{status}?pageNumber=2&pageSize=50
            workOrderGroup.MapGet("/", GetWorkOrderByStatusAsync)
            .WithName("GetWorkOrderByStatus");

            // POST: /api/workorders
            workOrderGroup.MapPost("/", CreateWorkOrderAsync)
            .WithName("CreateWorkOrder");

            // PUT: /api/workorders/{workOrderId}
            workOrderGroup.MapPut("/{workOrderId:string}", UpdateWorkOrderAsync)
            .WithName("UpdateWorkOrder");

            // PUT: /api/workorders/{workOrderId}?status=Completed
            workOrderGroup.MapPut("/{workOrderId:string}", UpdateWorkOrderStatusAsync)
            .WithName("UpdateWorkOrderStatus");

            // DELETE: /api/workorders
            workOrderGroup.MapDelete("/{workOrderId:string}", DeleteExternalSystemAsync)
            .WithName("DeleteWorkOrder");
        }

        // This will help with unit testing.
        // Get All Work Orders
        public static async Task<IResult> GetAllWorkOrdersAsync(IWorkOrderService service)
        {
            var workOrders = await service.GetAllAsync();
            return Results.Ok(workOrders);
        }

        // Get Work Order By Id
        public static async Task<Results<Ok<WorkOrder>, NotFound>> GetWorkOrdersById(string workOrderId, IWorkOrderService service)
        {
            var workOrder = await service.GetByIdAsync(workOrderId);
            return workOrder is not null ? TypedResults.Ok(workOrder) : TypedResults.NotFound();
        }

        // Get Work Order By Work Order Status
        public static async Task<IResult> GetWorkOrderByStatusAsync(WorkOrderStatusType status, int pageNumber, int pageSize, IWorkOrderService service)
        {
            var workOrders = await service.GetByStatusAsync(status, pageNumber, pageSize);
            return TypedResults.Ok(workOrders);
        }

        // Create Work Order
        public static async Task<IResult> CreateWorkOrderAsync(WorkOrder workOrder, IWorkOrderService workOrdersService)
        {
            var createdWorkOrder = await workOrdersService.CreateAsync(workOrder);
            return TypedResults.Created($"/api/workorders/{createdWorkOrder.WorkOrderId}");
        }

        // Update Work Order
        public static async Task<Results<NoContent, NotFound>> UpdateWorkOrderAsync(string workOrderId, WorkOrder workOrder, IWorkOrderService workOrdersService)
        {
            var updatedWorkOrder = await workOrdersService.UpdateAsync(workOrderId, workOrder);
            return updatedWorkOrder ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        // Update Work Order Status
        public static async Task<Results<NoContent, NotFound>> UpdateWorkOrderStatusAsync(string workOrderId, WorkOrderStatusType status, IWorkOrderService workOrdersService)
        {
            var updatedWorkOrder = await workOrdersService.UpdateWorkOrderStatusAsync(workOrderId, status);
            return updatedWorkOrder ? TypedResults.NoContent() : TypedResults.NotFound();
        }

        // Delete External Systems
        public static async Task<Results<NoContent, NotFound>> DeleteExternalSystemAsync(string workOrderId, IWorkOrderService workOrdersService)
        {
            var removedWorkOrder = await workOrdersService.DeleteAsync(workOrderId);
            return removedWorkOrder ? TypedResults.NoContent() : TypedResults.NotFound();
        }

    }
}
