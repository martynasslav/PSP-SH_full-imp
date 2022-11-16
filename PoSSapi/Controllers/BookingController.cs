using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : GenericController<Booking>
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] string? customerId,
            [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0)
            {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0)
            {
                return BadRequest("pageNum must be greater than or equal to 0");
            }

            var objectList = new Booking[itemsPerPage];
            for (int i = 0; i < itemsPerPage; i++)
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

            return Ok(objectList);
        }
    }
}
