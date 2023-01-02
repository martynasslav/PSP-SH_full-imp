using Enums;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreatePaymentDto
    {
        public byte FinancialDocuments { get; set; }
        public string OrderId { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CustomerId { get; set; }
        public PaymentType Type { get; set; }
    }
}