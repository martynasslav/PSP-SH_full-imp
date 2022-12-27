namespace Classes;

using Enums;
using System.ComponentModel.DataAnnotations;

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
    public string CustomerId { get; set; }
    [Required]
    public string LocationId { get; set; }
}
