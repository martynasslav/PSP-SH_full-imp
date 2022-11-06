namespace Classes;

using Enums;

public class Booking
{
    public string Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PeopleCount { get; set; }
    public BookingState State { get; set; }
    public Customer Customer { get; set; }
    public Location Location { get; set; }
}
