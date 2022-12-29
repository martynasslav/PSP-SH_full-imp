using System.ComponentModel.DataAnnotations;

#pragma warning disable 8618

namespace PoSSapi.Classes;

public class Location
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
}
