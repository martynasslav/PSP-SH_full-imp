using Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#pragma warning disable 8618

namespace Classes;

public class Booking
{
    [Key]
    public string Id { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int PeopleCount { get; set; }
    [Required]
    public BookingState State { get; set; }
    [Required]
    [ForeignKey("Customer")]
    public string CustomerId { get; set; }
    [Required]
    [ForeignKey("Location")]
    public string LocationId { get; set; }
}
