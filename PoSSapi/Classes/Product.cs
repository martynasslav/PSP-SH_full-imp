using System.ComponentModel.DataAnnotations;

namespace Classes;

public class Product
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public decimal Tax { get; set; }
    [Required]
    public string CategoryId { get; set; }
    [Required]
    public string LocationId { get; set; }
}
