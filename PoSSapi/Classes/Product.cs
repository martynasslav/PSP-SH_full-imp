using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

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
    [ForeignKey("Category")]
    public string CategoryId { get; set; }
    [Required]
    [ForeignKey("Location")]
    public string LocationId { get; set; }
}
