using Enums;

namespace Classes;

public class ProductOrder : Order
{
    public int TableNumber { get; set; }
    public decimal Tips { get; set; }
    public ProductOrderType OrderType { get; set; }
}
