using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Dtos;
using System.ComponentModel.DataAnnotations;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingController : GenericController<Booking>
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet()]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] string? customerId,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0)
        {
            return BadRequest("itemsPerPage must be greater than 0");
        }
        if (pageNum < 0)
        {
            return BadRequest("pageNum must be 0 or greater");
        }

        int totalItems = 20;
        int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

        var objectList = new Booking[itemsToDisplay];
        for (int i = 0; i < itemsToDisplay; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Booking>();
            if (locationId != null)
            {
                objectList[i].LocationId = locationId;
            }
            if (customerId != null)
            {
                objectList[i].CustomerId = customerId;
            }
        }

        ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }


    /** <summary>See a specific customers bookings</summary>
         * <param name="customerId" example="">Id of the customer you wish to see bookings of</param>
         */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("customer")]
    public ActionResult GetCustomerBookings([FromQuery][Required] string customerId)
    {
        return Ok(RandomGenerator.GenerateRandom<CustomerBookingDto>());
    }
}

