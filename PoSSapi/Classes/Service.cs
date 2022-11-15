namespace Classes;

public class Service
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? EmployeeId { get; set; }
    public decimal Price { get; set; }
    public Object Duration { get; set; } //Not specified in the diagram
    public Category Category { get; set; }
}
