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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
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
            
            var objectList = new Customer[itemsToDisplay];
            for (int i = 0; i < itemsToDisplay; i++)
            {
                objectList[i] = RandomGenerator.GenerateRandom<Customer>();
            }

            ReturnObject returnObject = new ReturnObject { totalItems = totalItems, itemList = objectList };
            return Ok(returnObject);
        }

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
