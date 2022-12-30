using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Dtos;
using System.ComponentModel.DataAnnotations;
using PoSSapi.Repository;
using PoSSapi.Dtos;

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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllBookings")]
    public ActionResult GetAllBookings([FromQuery] string? locationId, [FromQuery] string? customerId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0)
            return BadRequest("itemsPerPage must be greater than 0");

        if (pageNum < 0)
            return BadRequest("pageNum must be 0 or greater");

        var bookings = _bookingRepository.GetAllBookings();

        if (locationId != null)
        {
            bookings = bookings.Where(b => b.LocationId == locationId);
        }

        if (customerId != null)
        {
            bookings = bookings.Where(b => b.CustomerId == customerId);
        }

        bookings = bookings.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(bookings);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Booking))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetBooking")]
    public ActionResult<Booking> GetBooking(string id)
    {
        var booking = _bookingRepository.GetBbooking(id);

        if(booking == null)
        {
            return NotFound();
        }

        return booking;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(Name = "CreateBooking")]
    public ActionResult<Booking> CreateBooking(CreateBookingDto newBooking) 
    {
        var booking = new Booking()
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = newBooking.StartDate,
            EndDate = newBooking.EndDate,
            PeopleCount = newBooking.PeopleCount,
            State = newBooking.State,
            CustomerId = newBooking.CustomerId,
            LocationId = newBooking.LocationId
        };

        _bookingRepository.CreateBooking(booking);

        return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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