namespace Classes;

public class OrderService
{
    public string Id { get; set; }
    public Product Product { get; set; }
    public float Quantity { get; set; }
    public string? Details { get; set; }
    public ServiceOrder Order { get; set; }
    public Service Service { get; set; }
}
