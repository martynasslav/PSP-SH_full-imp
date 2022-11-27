using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : GenericController<Category>
    {
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalItems = 20;  
            int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

            var objectList = new Category[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Category>();
            }

            ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
            return Ok(returnObject);
        }
    }
}
