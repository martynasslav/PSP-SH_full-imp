﻿using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Enums;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : GenericController<Payment>
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? orderId,
            [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        { 
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }

            int totalItems = 20;  
            int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

            var objectList = new Payment[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; i++)
            {
                objectList[i] = RandomGenerator.GenerateRandom<Payment>();
                if (orderId != null)
                {
                    objectList[i].OrderId = orderId;
                }
            }

            ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
            return Ok(returnObject);
        }
    }
}
