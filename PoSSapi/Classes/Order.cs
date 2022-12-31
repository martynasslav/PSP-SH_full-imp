using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class Order
{
    [Key]
    public string Id { get; set; }
    [Required]
    public OrderStatusState OrderStatus { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime FinishDate { get; set; }
    [Required]
    [ForeignKey("Customer")]
    public string? CustomerId { get; set; }
    [Required]
    [ForeignKey("Employee")]
    public string EmployeeId { get; set; }
    [Required]
    public string Payments { get; set; }
}
