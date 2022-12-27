using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly DbEntities _dbEntities;

        public BookingRepository(DbEntities dbEntities) 
        {
            _dbEntities = dbEntities;
        }

        public void CreateBooking(Booking booking)
        {
            _dbEntities.Bookings.Add(booking);
            _dbEntities.SaveChanges();
        }

        public void DeleteBooking(Booking booking)
        {
            _dbEntities.Bookings.Remove(booking);
            _dbEntities.SaveChanges();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _dbEntities.Bookings;
        }

        public Booking GetBbooking(string id)
        {
            return _dbEntities.Bookings.Find(id);
        }

        public void UpdateBooking(Booking booking)
        {
            var _booking = _dbEntities.Bookings.Find(booking.Id);
            _booking.Id = booking.Id;
            _booking.StartDate = booking.StartDate;
            _booking.EndDate = booking.EndDate;
            _booking.PeopleCount= booking.PeopleCount;
            _booking.State = booking.State;
            _booking.CustomerId = booking.CustomerId;
            _booking.LocationId = booking.LocationId;
            _dbEntities.Bookings.Update(_booking);
            _dbEntities.SaveChanges();
        }
    }
}
