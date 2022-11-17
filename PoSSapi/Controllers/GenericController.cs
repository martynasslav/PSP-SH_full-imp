using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;
using System.Linq;

namespace PoSSapi.Controllers
{
    public abstract class GenericController<T>: ControllerBase where T: new()
    {
        protected class ReturnObject {
            public int totalItems {get; set;}
            public T[] itemList {get; set;}
        }

        protected static class ControllerTools {
            public static int calculateItemsToDisplay(int itemsPerPage, int pageNum, int totalItems) {
                //determine how many items should be displayed
                //case 1: there are exactly as many or more items than we are trying to display
                if ((pageNum + 1) * itemsPerPage <= totalItems) {
                    return itemsPerPage;
                }
                //case 2: there are less items than we are trying to display, but there are still some we haven't displayed
                else if (pageNum * itemsPerPage < totalItems) {
                    return totalItems - pageNum * itemsPerPage;
                }
                //case 3: we have displayed all items. display empty list
                else {
                    return 0;
                }
            }
        }

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
