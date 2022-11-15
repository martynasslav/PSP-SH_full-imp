using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;
using System.Linq;

namespace PoSSapi.Controllers
{
    public abstract class GenericController<T>: ControllerBase where T: new()
    {
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return Ok(RandomGenerator.GenerateRandom<T>(id));
        }

        [HttpPost]
        public ActionResult Post([FromBody] T newObject)
        {
            return CreatedAtAction(nameof(Get), new { id = "1" }, newObject);
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] T updatedObject)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
