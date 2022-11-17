using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Tools;
using Dtos;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : GenericController<Customer>
    {
        /** <summary>Gets customers payments by id</summary>
         * <param name="customerId" example="">Id of the customer whose payments you wish to see</param>
         */
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("spending")]
        public ActionResult GetCustomerSpendingById([FromQuery] string customerId)
        {
            var spendingDto = RandomGenerator.GenerateRandom<SpendingDto>();
            spendingDto.CustomerId = customerId;
            spendingDto.Payments = new List<PaymentDto>();

            var amountOfPayments = new Random().Next(1, 10);

            for (int i = 0; i < amountOfPayments; i++)
            {
                //WHY ARENT THESE RANDOM!!!???
                spendingDto.Payments.Add(RandomGenerator.GenerateRandom<PaymentDto>());
            }

            return Ok(spendingDto);
        }
    }
}
