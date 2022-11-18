namespace Dtos;

public record struct SpendingDto
{
    public string CustomerId { get; set; }
    public List<PaymentDto> Payments { get; set; }
}
