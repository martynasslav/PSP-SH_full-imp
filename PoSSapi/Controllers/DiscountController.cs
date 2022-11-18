using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Enums;
using System.ComponentModel.DataAnnotations;

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
             [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
        {
            if (itemsPerPage <= 0)
            {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0)
            {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalItems = 20;
            int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

            var objectList = new Discount[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; i++)
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

            ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
            return Ok(returnObject);
        }

        /** <summary>Send an email promotion of this discount</summary>
             * <param name="discountId" example="">Id of the discount you wish to promote</param>
             */

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("promote")]
        public ActionResult PromoteDiscountToCustomers([FromQuery][Required] string discountId)
        {
            return Ok();
        }
    }
}
