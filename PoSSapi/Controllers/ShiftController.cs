using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : GenericController<Shift>
    {
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? employeeId)
        {
            var objectList = new Shift[] { RandomGenerator.GenerateRandom<Shift>() };
            if (employeeId != null)
            {
                objectList[0].EmployeeId = employeeId;
            }
            return Ok(objectList);
        }
    }
}
