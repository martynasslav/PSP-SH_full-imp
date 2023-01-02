using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class Customer
{
    [Key]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public DateTime Birthday { get; set; }
    public string Address { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public long CardNumber { get; set; }
}
