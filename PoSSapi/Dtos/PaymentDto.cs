namespace Dtos;

public record struct PaymentDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
