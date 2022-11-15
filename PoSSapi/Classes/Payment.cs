using Enums;

namespace Classes;

public class Payment
{
    public string Id { get; set; }
    public List<byte[]> FinancialDocuments { get; set; }
    public string OrderId { get; set; }
    public DateTime CompletionDate { get; set; }
    public string CustomerId { get; set; }
    public PaymentType Type { get; set; }
}
