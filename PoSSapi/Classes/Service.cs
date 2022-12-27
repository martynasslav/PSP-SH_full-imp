using System.ComponentModel.DataAnnotations;

namespace Classes;

public class Service
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? EmployeeId { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int Duration { get; set; } // in seconds
    [Required]
    public string CategoryId { get; set; }
    [Required]
    public string LocationId { get; set; }
}
