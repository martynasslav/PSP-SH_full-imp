using Classes;

namespace Dtos;

public record struct CustomerBookingDto
{
    public string CustomerId { get; set; }
    public List<Booking> Bookings { get; set; }
}
