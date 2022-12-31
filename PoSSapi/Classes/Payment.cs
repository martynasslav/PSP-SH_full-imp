using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class Payment
{
    [Key]
    public string Id { get; set; }
    [Required]
    public byte[] FinancialDocuments { get; set; }
    [Required]
    [ForeignKey("Order")]
    public string OrderId { get; set; }
    [Required]
    public DateTime CompletionDate { get; set; }
    [Required]
    [ForeignKey("Customer")]
    public string CustomerId { get; set; }
    [Required]
    public PaymentType Type { get; set; }
}
