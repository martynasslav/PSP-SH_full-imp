namespace Classes;

public class Order
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public Customer? Customer { get; set; }
    public Employee Employee { get; set; }
    public Payment Payment { get; set; }
}
