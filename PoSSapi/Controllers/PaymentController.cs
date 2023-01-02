using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Enums;
using Dtos;
using PoSSapi.Controllers;
using System.ComponentModel.DataAnnotations;
using PoSSapi.Repositories;
using PoSSapi.Dtos;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : ControllerBase
{
    private IPaymentRepository _paymentRepository;

    public PaymentController(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllPayments")]
    public ActionResult GetAllPayments([FromQuery] string? orderId,
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

        var payments = _paymentRepository.GetAllPayments();

        if (orderId != null)
        {
            payments = payments.Where(p => p.OrderId == orderId);
        }

        payments = payments.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(payments);
    }

    /** <summary>Only to be used when paying by card</summary>
      */
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("pay-card")]
    public ActionResult PayForOrderWithCard([FromBody] CreatePaymentDto newPayment)
    {
        var payment = new Payment()
        {
            Id = Guid.NewGuid().ToString(),
            FinancialDocuments = newPayment.FinancialDocuments,
            OrderId = newPayment.OrderId,
            CompletionDate = newPayment.CompletionDate,
            CustomerId = newPayment.CustomerId,
            Type = (PaymentType)1
        };

        _paymentRepository.CreatePayment(payment);
        
        return CreatedAtAction("GetCheck", new { id = payment.Id}, payment);
    }

    /** <summary>Only to be used when paying with cash</summary>
      */
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("pay-cash")]
    public ActionResult MarkOrderAsPaid([FromBody] CreatePaymentDto newPayment)
    {
        var payment = new Payment()
        {
            Id = Guid.NewGuid().ToString(),
            FinancialDocuments = newPayment.FinancialDocuments,
            OrderId = newPayment.OrderId,
            CompletionDate = newPayment.CompletionDate,
            CustomerId = newPayment.CustomerId,
            Type = (PaymentType)0
        };

        _paymentRepository.CreatePayment(payment);

        return CreatedAtAction("GetCheck", new { id = payment.Id }, payment);
    }

    /** <summary>Gets all payments for specific customer</summary>
      */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/invoice/{customerId}")]
    public ActionResult<Payment> GetCustomerPayment([FromQuery][Required] string? customerId)
    {
        var payment = _paymentRepository.GetAllPayments();

        payment = payment.Where(p => p.CustomerId == customerId);
        
        if (payment == null)
        {
            return NotFound();
        }
        
        return Ok(payment);
    }

    /** <summary>Returns a check</summary>
    * <param name="id" example="">Id of the payment that you want the check of</param>
    */
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Payment))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/check")]
    public ActionResult<Payment> GetCheck(string id)
    {
        var payment = _paymentRepository.GetPayment(id);

        if (payment == null)
        {
            return NotFound();
        }

        return payment;
    }
}

