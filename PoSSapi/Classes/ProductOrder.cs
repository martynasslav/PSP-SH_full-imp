using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class ProductOrder : Order
{
    [Required]
    [ForeignKey("OrderProduct")]
    public int TableNumber { get; set; }
    [Required]
    public decimal Tips { get; set; }
    [Required]
    public ProductOrderType OrderType { get; set; }
}
