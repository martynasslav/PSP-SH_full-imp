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
<<<<<<< Updated upstream
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
=======
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentRepository _paymentRepository;

        public CustomerController(ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
        {
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
        }


        /// <summary>
        /// Get customers by name
        /// </summary>
        /// <response code="200">Information about customers returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        /** <summary>Gets customers payments by id</summary>
         * <param name="id" example="">Id of the customer whose payments you wish to see</param>
         */
=======
        /// <summary>Gets customers payments by id</summary>
        /// <param name="id">Id of the customer whose payments you wish to see</param>
        /// <response code="200">Payments returned.</response>
>>>>>>> Stashed changes
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/spending")]
        public ActionResult GetCustomerSpendingById(string id)
        {
<<<<<<< Updated upstream
            var spendingDto = RandomGenerator.GenerateRandom<SpendingDto>();
            spendingDto.CustomerId = id;
            spendingDto.Payments = new List<PaymentDto>();

            var amountOfPayments = new Random().Next(1, 10);

            for (int i = 0; i < amountOfPayments; i++)
=======
            var payments = _paymentRepository.GetAllPayments().Where(x => x.CustomerId == id);
            if (!payments.Any())
>>>>>>> Stashed changes
            {
                //WHY ARENT THESE RANDOM!!!???
                spendingDto.Payments.Add(RandomGenerator.GenerateRandom<PaymentDto>());
            }
<<<<<<< Updated upstream

            return Ok(spendingDto);
=======
            return Ok(payments);
>>>>>>> Stashed changes
        }
    }
}
