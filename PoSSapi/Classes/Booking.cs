namespace Classes;

using Enums;

public class Booking
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PeopleCount { get; set; }
    public BookingState State { get; set; }
    public string CustomerId { get; set; }
    public string LocationId { get; set; }
}
