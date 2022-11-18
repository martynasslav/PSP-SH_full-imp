using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Enums;
using Dtos;
using PoSSapi.Controllers;
using System.ComponentModel.DataAnnotations;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : GenericController<Payment>
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet()]
    public ActionResult GetAll([FromQuery] string? orderId,
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

        var objectList = new Payment[itemsToDisplay];
        for (int i = 0; i < itemsToDisplay; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Payment>();
            if (orderId != null)
            {
                objectList[i].OrderId = orderId;
            }
        }

        ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
        return Ok(returnObject);
    }

    /** <summary>Only to be used when paying by card</summary>
      */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("pay-card")]
    public ActionResult PayForOrderWithCard([FromBody][Required] IncomingPaymentByCardDto payment)
    {
        return Ok();
    }

    /** <summary>Only to be used when paying with cash</summary>
      */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("pay-cash")]
    public ActionResult MarkOrderAsPaid([FromBody][Required] IncomingPaymentDto payment)
    {
        return Ok();
    }

    /** <summary>Emails a specific invoice to a specific customer</summary>
    * <param name="id" example="">Id of the payment that you want to send the invoice of</param>
    * <param name="customerId" example="">Id of the customer whose recieving the invoice</param>
    */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/invoice/{customerId}")]
    public ActionResult SendInvoiceToCustomer(string id, string customerId)
    {
        return Ok();
    }

    /** <summary>Returns a check</summary>
    * <param name="id" example="">Id of the payment that you want the check of</param>
    */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/check")]
    public ActionResult GetCheck(string id)
    {
        return Ok();
    }
}

