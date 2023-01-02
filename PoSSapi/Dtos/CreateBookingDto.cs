using Enums;

#pragma warning disable 8618

namespace PoSSapi.Dtos
{
    public record CreateBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleCount { get; set; }
        public BookingState State { get; set; }
        public string CustomerId { get; set; }
        public string LocationId { get; set; }
    }
}
