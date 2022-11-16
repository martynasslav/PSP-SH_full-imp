using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;
using System.Linq;

namespace PoSSapi.Controllers
{
    public abstract class GenericController<T>: ControllerBase where T: new()
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {
            return Ok(RandomGenerator.GenerateRandom<T>(id));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult Post([FromBody] T newObject)
        {
            return CreatedAtAction(nameof(Get), new { id = "1" }, newObject);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] T updatedObject)
        {
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            return Ok();
        }
    }
}
