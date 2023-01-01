using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Dtos;
using PoSSapi.Repositories;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        // private readonly IPaymentRepository _paymentRepository;


        public CustomerController(ICustomerRepository customerRepository )
        {
            _customerRepository = customerRepository;
        }

        /* Uncomment this after payment is implemented and remove the constructor 
        public CustomerController(ICustomerRepository customerRepository, IPaymentRepository paymentRepository)
        {
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
        }
        */

        /// <summary>
		/// Get customers by name
		/// </summary>
		/// <response code="200">Information about customers returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? name, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
        {
            if (itemsPerPage <= 0)
            {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0)
            {
                return BadRequest("pageNum must be 0 or greater");
            }

            var customers = _customerRepository.GetCustomers();

            if(name != null)
            {
                customers = customers.Where(x => x.Name == name);
            }
            customers = customers.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

            return Ok(customers);
        }

        /// <summary>
		/// Get customer by ID
		/// </summary>
		/// <response code="200">Information about customer returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult GetCustomerById(string id)
        {
             var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
		/// Create a new customer
		/// </summary>
		/// <response code="201">Customer created.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult CreateCustomer([FromBody] CustomerCreationDto newCustomer)
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid().ToString(),
                Name = newCustomer.Name,
                Surname = newCustomer.Surname,
                Birthday = newCustomer.Birthday,
                Address = newCustomer.Address,
                Email = newCustomer.Email,
                CardNumber = newCustomer.CardNumber
            };
            _customerRepository.InsertCustomer(customer);
            _customerRepository.Save();

            return CreatedAtAction(nameof(GetCustomerById), new {id = customer.Id}, customer);
        }

        /// <summary>
		/// Update customer information
		/// </summary>
		/// <response code="200">Customer information updated.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public ActionResult UpdateCustomer([FromBody] Customer updatedCustomer)
        {
            var customer = _customerRepository.GetCustomerById(updatedCustomer.Id);
            if(customer == null)
            {
                return NotFound();
            }
            _customerRepository.UpdateCustomer(updatedCustomer);
            _customerRepository.Save();

            return Ok();
        }

        /// <summary>
		/// Delete customer
		/// </summary>
		/// <response code="204">Customer deleted.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if(customer == null)
            {
                return NotFound();
            }
            _customerRepository.DeleteCustomer(customer);
            _customerRepository.Save();

            return NoContent();
        }


        // TODO - This part. Need Payment implementation
        /// <summary>Gets customers payments by id</summary>
        /// <param name="id">Id of the customer whose payments you wish to see</param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/spending")]
        public ActionResult GetCustomerSpendingById(string id)
        {
            /* Placeholder code untill payment is implemented. Unsure if this will work.
            var payments = _paymentRepository.GetPayments().Where(x => x.CustomerId == id);
            if (!payments.Any())
            {
                return NotFound();
            }
            return Ok(payments);*/

            return Ok();
        }
    }
}
