using Enums;

namespace Classes;

public class OrderProduct
{
    public int TableNumber { get; set; }
    public Product Product { get; set; }
    public float Quantity { get; set; }
    public string? Details { get; set; }
    public Product Product { get; set; }
    public ProductOrder Order { get; set; }
}
