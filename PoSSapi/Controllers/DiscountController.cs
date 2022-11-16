using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Enums;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : GenericController<Discount>
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] DiscountTargetType? discountTarget, [FromQuery] string? targetId,
             [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0)
            {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0)
            {
                return BadRequest("pageNum must be greater than or equal to 0");
            }

            var objectList = new Discount[itemsPerPage];
            for (int i = 0; i < itemsPerPage; i++)
            {
                objectList[i] = RandomGenerator.GenerateRandom<Discount>();
                if (discountTarget != null)
                {
                    objectList[i].TargetType = (DiscountTargetType)discountTarget;
                }
                if (targetId != null)
                {
                    objectList[i].TargetId = targetId;
                }
            }

            return Ok(objectList);
        }
    }
}
