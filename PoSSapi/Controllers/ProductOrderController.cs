using System.ComponentModel.DataAnnotations;
using Classes;
using Enums;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductOrderController : GenericController<ProductOrder>
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] OrderStatusState? status,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }

        var objectList = new ProductOrder[itemsPerPage];
        for (var i = 0; i < itemsPerPage; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<ProductOrder>();
            objectList[i].OrderStatus = status ?? OrderStatusState.New;
        }

        return Ok(objectList);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public ActionResult ChangeStatus(string id, [FromQuery][Required] OrderStatusState? status)
    {
        var productOrder = RandomGenerator.GenerateRandom<ProductOrder>(id);
        productOrder.OrderStatus = status ?? OrderStatusState.New;

        return Ok();
    }
    
}
