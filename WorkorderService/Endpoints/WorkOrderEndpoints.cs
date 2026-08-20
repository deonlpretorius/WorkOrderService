using WorkOrderService.Models.WorkOrders;
//using WorkOrderService.

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
        public static void MapWorkOrderEndpoints(this IEndpointRouteBuilder routes)
        {
            // Makes use of Route Groups for a cleaner API.
            var workOrderGroup = routes.MapGroup("/api/workorders")
                                       .WithTags("WorkOrders");

            // workOrderGroup.MapGet("/{workOrderId:string}", async (string WorkOrder, IWorkOrdersService ))
        }
    }
}
