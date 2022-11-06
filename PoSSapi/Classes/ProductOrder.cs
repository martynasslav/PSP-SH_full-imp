using Enums;

namespace Classes;

public class ProductOrder : Order
{
    public int TableNumber { get; set; }
    public Decimal Tips { get; set; }
    public ProductOrderType OrderType { get; set; }
    public OrderStatusState OrderStatus { get; set; }
}
