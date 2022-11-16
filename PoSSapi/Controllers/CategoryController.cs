using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : GenericController<Category>
    {
        private class ReturnObject {
            public int totalCategories {get; set;}
            public Category[] categoryList {get; set;}
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalCategories = 20;
            if (itemsPerPage * pageNum >= totalCategories) {
                return NotFound("Requested page does not exist");
            }

            int itemsToDisplay = Math.Min(totalCategories - 1, itemsPerPage * (pageNum + 1) - 1) % itemsPerPage + 1;
            var objectList = new Category[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; ++i) {
                objectList[i] = RandomGenerator.GenerateRandom<Category>();
            }

            ReturnObject returnObject = new ReturnObject {totalCategories = totalCategories, categoryList = objectList};
            return Ok(returnObject);
        }
    }
}
