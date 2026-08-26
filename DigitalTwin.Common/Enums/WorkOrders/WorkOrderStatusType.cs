/// <summary>
/// Namespace <c>DigitalTwin.Enums.WorkOrders</c> contains the enumerations for the application.
/// </summary>
namespace DigitalTwin.Enums.WorkOrders
{
    /// <summary>
    /// Enum <c>WorkOrderStatusType</c> represents the status of a work order.
    /// </summary>
    public enum WorkOrderStatusType : int
    {
        Pending = 1,
        Accepted = 2,
        Completed = 3,
        Cancelled = 4
    }
}
