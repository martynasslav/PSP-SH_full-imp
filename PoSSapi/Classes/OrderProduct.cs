using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class OrderProduct
{
    [Key]
    public int TableNumber { get; set; }
    [Required]
    [ForeignKey("Product")]
    public string ProductId { get; set; }
    [Required]
    public float Quantity { get; set; }
    public string? Details { get; set; }
    [Required]
    [ForeignKey("Order")]
    public string OrderId { get; set; }
}
