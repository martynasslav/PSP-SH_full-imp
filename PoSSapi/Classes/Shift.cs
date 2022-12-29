using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace PoSSapi.Classes;

public class Shift
{
    [Key]
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime FinishDate { get; set; }
    public DateTime? CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    [Required]
    [ForeignKey("Employee")]
    public string EmployeeId { get; set; }
}
