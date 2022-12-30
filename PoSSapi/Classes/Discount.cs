using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Enums;

#pragma warning disable 8618

namespace Classes;

public class Discount
{
    [Key]
    public string Id { get; set; }
    [Required]
    public DiscountType Type { get; set; }
    [Required]
    public decimal Amount { get; set; }
    [Required]
    public DiscountTargetType TargetType { get; set; }
    [Required]
    public string TargetId { get; set; }
}
