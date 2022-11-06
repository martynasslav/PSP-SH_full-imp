using Enums;

namespace Classes;

public class Payment
{
    public string Id { get; set; }
    public List<byte[]> FinancialDocuments { get; set; }
    public Order Order { get; set; }
    public DateTime CompletionDate { get; set; }
    public Customer Customer { get; set; }
    public PaymentType Type { get; set; }
}
