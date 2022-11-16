using Enums;

namespace Classes;

public class Order
{
    public string Id { get; set; }
    public OrderStatusState OrderStatus { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public string? CustomerId { get; set; }
    public string EmployeeId { get; set; }
    public List<string> Payments { get; set; }
}
