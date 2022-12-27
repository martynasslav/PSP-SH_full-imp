using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Dtos;
using System.ComponentModel.DataAnnotations;
using PoSSapi.Repository;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : ControllerBase
{
    private IBookingRepository _bookingRepository;

    public BookingController(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }

    [ProducesResponseType(200)]
    [HttpGet(Name = "GetAllBookings")]
    public IEnumerable<Booking> GetAllBookings([FromQuery] string? locationId, [FromQuery] string? customerId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        var bookings = _bookingRepository.GetAllBookings();

        if (locationId != null)
        {
            bookings = bookings.Where(b => b.LocationId == locationId);
        }
        if (customerId != null)
        {
            bookings = bookings.Where(b => b.CustomerId == customerId);
        }

        return bookings.Skip(pageNum).Take(itemsPerPage);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet("{id}", Name="GetBooking")]
    public ActionResult<Booking> GetBooking(string id)
    {
        var booking = _bookingRepository.GetBbooking(id);

        if(booking == null)
        {
            return NoContent();
        }

        return booking;
    }

    [ProducesResponseType(201)]
    [HttpPost(Name = "CreateBooking")]
    public ActionResult<Booking> CreateBooking(Booking booking) 
    {
        _bookingRepository.CreateBooking(booking);
        return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpPut("{id}", Name = "UpdateBooking")]
    public ActionResult<Booking> UpdateBooking(string id, Booking booking)
    {
        var _booking = _bookingRepository.GetBbooking(id);

        if (_booking == null)
        {
            return NotFound();
        }

        booking.Id = _booking.Id;
        _bookingRepository.UpdateBooking(booking);
        return Ok(booking);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpDelete("{id}", Name = "DeleteBooking")]
    public ActionResult<Booking> DeleteBooking(string id) 
    {
        var booking = _bookingRepository.GetBbooking(id);

        if (booking == null)
        {
            return NotFound();
        }

        _bookingRepository.DeleteBooking(booking);

        return Ok();
    }
}