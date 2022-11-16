using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : GenericController<Shift>
    {
        private class ReturnObject {
            public int totalShifts {get; set;}
            public Shift[] ShiftList {get; set;}
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? employeeId, [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalShifts = 20;
            if (itemsPerPage * pageNum >= totalShifts) {
                return NotFound("Requested page does not exist");
            }

            int itemsToDisplay = Math.Min(totalShifts - 1, itemsPerPage * (pageNum + 1) - 1) % itemsPerPage + 1;
            var objectList = new Shift[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Shift>();

                if (employeeId != null) {
                    objectList[i].EmployeeId = employeeId;
                }
            }

            ReturnObject returnObject = new ReturnObject {totalShifts = totalShifts, ShiftList = objectList};
            return Ok(returnObject);
        }
    }
}
