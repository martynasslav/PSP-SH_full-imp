namespace Classes;

public class Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Tax { get; set; }
    public Discount Discount { get; set; }
    public Category Category { get; set; }
}
