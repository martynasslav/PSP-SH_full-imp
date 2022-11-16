using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : GenericController<Product>
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] string? categoryId, 
        [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }

        var objectList = new Product[itemsPerPage];
        for (var i = 0; i < itemsPerPage; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Product>();
            
            if (locationId != null)
            {
                objectList[i].LocationId = locationId;
            }
            
            if (categoryId != null)
            {
                objectList[i].CategoryId = categoryId;
            }
        }
        
        return Ok(objectList);
    }
}
