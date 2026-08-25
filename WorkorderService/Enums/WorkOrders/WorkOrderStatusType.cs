/// <summary>
/// Namespace <c>WorkOrderService.Enums</c> contains the enumerations for the application.
/// </summary>
namespace DigitalTwin.WebAPI.Enums.WorkOrders
{
    /// <summary>
    /// Enum <c>WorkOrderStatus</c> represents the status of a work order.
    /// </summary>
    public enum WorkOrderStatusType : int
    {
        Pending = 1,
        Accepted = 2,
        Completed = 3,
        Cancelled = 4
    }
}
