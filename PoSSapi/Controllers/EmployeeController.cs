using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : GenericController<Employee>
    {
        private class ReturnObject {
            public int totalEmployees {get; set;}
            public Employee[] employeeList {get; set;}
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? username, [FromQuery] bool? isManager, [FromQuery] string? locationId, [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalEmployees;
            if (username != null) {
                totalEmployees = 1;
            }
            else {
                totalEmployees = 20;
            }

            if (itemsPerPage * pageNum >= totalEmployees) {
                return NotFound("Requested page does not exist");
            }

            int itemsToDisplay = Math.Min(totalEmployees - 1, itemsPerPage * (pageNum + 1) - 1) % itemsPerPage + 1;
            var objectList = new Employee[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Employee>();

                if (username != null) {
                    objectList[i].Username = username;
                }
                if (isManager != null) {
                    objectList[i].IsManager = (bool)isManager;
                }
                if (locationId != null) {
                    objectList[i].LocationId = locationId;
                }
            }

            ReturnObject returnObject = new ReturnObject {totalEmployees = totalEmployees, employeeList = objectList};
            return Ok(returnObject);
        }
    }
}
