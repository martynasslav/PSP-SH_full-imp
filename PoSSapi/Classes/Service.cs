namespace Classes;

public class Service
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string? EmployeeId { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; } // in seconds
    public string CategoryId { get; set; }
    public string LocationId { get; set; }
}
