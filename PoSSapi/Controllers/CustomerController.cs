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
         * <param name="id" example="">Id of the customer whose payments you wish to see</param>
         */
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/spending")]
        public ActionResult GetCustomerSpendingById(string id)
        {
            var spendingDto = RandomGenerator.GenerateRandom<SpendingDto>();
            spendingDto.CustomerId = id;
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
