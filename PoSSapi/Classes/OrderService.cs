using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class OrderService
{
    [Key]
    public string Id { get; set; }
    [Required]
    [ForeignKey("Service")]
    public string ServiceId { get; set; }
    [Required]
    public float Quantity { get; set; }
    public string? Details { get; set; }
    [Required]
    [ForeignKey("Order")]
    public string OrderId { get; set; }
}
