using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class Category
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string ClientId { get; set; }
}
