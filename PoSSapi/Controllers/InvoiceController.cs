using Microsoft.AspNetCore.Mvc;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class InvoiceController : ControllerBase
{
    /** <summary>Emails a specific invoice to a specific customer</summary>
     * <param name="customerId" example="">Id of the customer whose recieving the invoice</param>
     * <param name="paymentId" example="">Id of the payment that you want to send the invoice of</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ActionResult SendInvoiceToCustomer([FromQuery] string customerId, [FromQuery] string paymentId)
    {
        return Ok();
    }
}
