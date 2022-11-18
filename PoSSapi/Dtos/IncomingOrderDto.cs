namespace Dtos;

public class IncomingPaymentDto
{
    public string OrderId { get; set; }
    public string CustomerId { get; set; }
    public decimal Amount { get; set; }
}

public record struct CardDetails
{
    public string CardHolderName { get; set; }
    public string CVC { get; set; } //Security cody comprised of 3 digits
    public DateTime ExpirationDate { get; set; }
    public string Iban { get; set; }
}

public class IncomingPaymentByCardDto : IncomingPaymentDto
{
    public CardDetails CardDetails { get; set; }
}

