using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : GenericController<Shift>
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? employeeId, [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalItems = 20;  
            int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

            var objectList = new Shift[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Shift>();

                if (employeeId != null) {
                    objectList[i].EmployeeId = employeeId;
                }
            }

            ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
            return Ok(returnObject);
        }
    }
}
