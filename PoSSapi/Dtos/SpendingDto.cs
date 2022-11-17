namespace Dtos;

public record struct SpendingDto
{
    public string CustomerId { get; set; }
    public List<PaymentDto> Payments { get; set; }
}

public record struct PaymentDto
{
    public string Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}
