/// <summary>
/// Namespace <c>WorkOrderService.Enums</c> contains the enumerations for the application.
/// </summary>
namespace WorkOrderService.Enums
{
    /// <summary>
    /// Enum <c>WorkOrderStatus</c> represents the status of a work order.
    /// </summary>
    public enum WorkOrderStatus : int
    {
        Pending = 1,
        Accepted = 2,
        Cancelled = 3
    }
}
