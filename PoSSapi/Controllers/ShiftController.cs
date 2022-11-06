using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Classes;
using Enums;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShiftController : ControllerBase
    {
        // GET /Shift
        [HttpGet]
        public ActionResult<Shift> Get()
        {
            // Return a list of shifts
            var shiftList = new List<Shift>();
            shiftList.Append(Shift.GenerateRandom());
            return Ok(shiftList);
        }

        // GET /Shift/5
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return Ok(Shift.GenerateRandom(id));
        }

        // POST /Shift
        [HttpPost]
        public ActionResult Post([FromBody] Shift shift)
        {
            return CreatedAtAction(nameof(Get), new { id = shift.Id }, shift);
        }

        // PUT /Shift/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Shift shift)
        {
            return Ok();
        }

        // DELETE /Shift/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
