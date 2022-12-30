using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Dtos;
using Enums;
using PoSSapi.Repositories;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        /// <summary>
		/// Get discounts
		/// </summary>
		/// <response code="200">Information about discounts returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? targetId, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
        {
            if (itemsPerPage <= 0)
            {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0)
            {
                return BadRequest("pageNum must be 0 or greater");
            }

            var discounts = _discountRepository.GetDiscounts();

            if(targetId == null)
            {
                discounts = discounts.Where(x => x.TargetId == targetId);
            }
            discounts = discounts.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

            return Ok(discounts);
        }

        /// <summary>
		/// Get discount by ID
		/// </summary>
		/// <response code="200">Information about discount returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Discount))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult GetDiscountById(string id)
        {
            var discount = _discountRepository.GetDiscountById(id);
            if(discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        /// <summary>
		/// Create a new discount
		/// </summary>
		/// <response code="201">Discount created.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Discount))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult CreateDiscount([FromBody] DiscountCreationDto newDiscount)
        {
            var discount = new Discount
            {
                Id = Guid.NewGuid().ToString(),
                Type = newDiscount.Type,
                Amount = newDiscount.Amount,
                TargetType = newDiscount.TargetType,
                TargetId = newDiscount.TargetId
            };
            _discountRepository.InsertDiscount(discount);
            _discountRepository.Save();

            return CreatedAtAction(nameof(GetDiscountById), new {id = discount.Id}, discount);
        }

        /// <summary>
		/// Update discount information
		/// </summary>
		/// <response code="200">Discount  information updated.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult UpdateDiscount(string id, [FromBody] DiscountCreationDto updatedDiscount)
        {
            var discount = _discountRepository.GetDiscountById(id);
            if(discount == null)
            {
                return NotFound();
            }
            discount.Amount = updatedDiscount.Amount;
            discount.TargetType = updatedDiscount.TargetType;
            discount.TargetId = updatedDiscount.TargetId;
            discount.Type = updatedDiscount.Type;

            _discountRepository.UpdateDiscount(discount);
            _discountRepository.Save();

            return Ok();
        }

        /// <summary>
		/// Delete a discount
		/// </summary>
		/// <response code="204">Discount deleted.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult DeleteDiscount(string id)
        {
            var discount = _discountRepository.GetDiscountById(id);
            if(discount == null)
            {
                return NotFound();
            }
            _discountRepository.DeleteDiscount(discount);
            _discountRepository.Save();

            return NoContent();
        }
    }
}
