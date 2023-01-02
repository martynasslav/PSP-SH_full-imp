﻿using Classes;

namespace PoSSapi.Repositories
{
    public interface IBookingRepository
    {
        void CreateBooking(Booking booking);
        Booking GetBbooking(string id);
        IEnumerable<Booking> GetAllBookings();
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
    }
}
