using Enums;

namespace Classes;

public class OrderProduct
{
    public int TableNumber { get; set; }
    public string ProductId { get; set; }
    public float Quantity { get; set; }
    public string? Details { get; set; }
    public string OrderId { get; set; }
}
