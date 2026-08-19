/// <summary>
/// Namespace <c>WorkOrderService.Enums</c> contains the enumerations.
/// </summary>
namespace WorkOrderService.Enums
{
    /// <summary>
    /// Enum <c>WorkOrderStatusType</c> represents the status of an work order.
    /// </summary>
    public enum WorkOrderStatus : int
    {
        Pending = 1,
        Accepted = 2,
        Cancelled = 3
    }
}
