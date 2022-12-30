using Microsoft.AspNetCore.Mvc;
using Classes;
using PoSSapi.Dtos;
using PoSSapi.Repositories;

namespace PoSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
		/// Get categories
		/// </summary>
		/// <response code="200">Information about categories returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet()]
        public ActionResult GetAll([FromQuery] string? name, [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
        {
            if (itemsPerPage <= 0) {
                return BadRequest("itemsPerPage must be greater than 0");
            }
            if (pageNum < 0) {
                return BadRequest("pageNum must be 0 or greater");
            }
            
            var categories = _categoryRepository.GetCategories();

            if(name != null)
            {
                categories = categories.Where(c => c.Name == name);
            }
            categories = categories.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

            return Ok(categories);
        }

        /// <summary>
		/// Get category by ID
		/// </summary>
		/// <response code="200">Information about category returned.</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult GetCategoryById(string id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
		/// Create category
		/// </summary>
		/// <response code="201">Category created.</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult CreateCategory([FromBody] CategoryCreationDto newCategory)
        {
            var category = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = newCategory.Name,
                ClientId = newCategory.ClientId
            };
            _categoryRepository.InsertCategory(category);
            _categoryRepository.Save();

            return CreatedAtAction(nameof(GetCategoryById), new {id = category.Id}, category);
        }

        /// <summary>
		/// Update category
		/// </summary>
		/// <response code="200">Category updated.</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public ActionResult UpdateCategory(string id, [FromBody] CategoryCreationDto updatedCategory)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            category.Name = updatedCategory.Name;
            category.ClientId = updatedCategory.ClientId;

            _categoryRepository.UpdateCategory(category);
            _categoryRepository.Save();

            return Ok();
        }

        /// <summary>
		/// Delete a category
		/// </summary>
		/// <response code="204">Category deleted.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(string id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if(category == null)
            {
                return NotFound();
            }
            _categoryRepository.DeleteCategory(category);
            _categoryRepository.Save();

            return NoContent();
        }
    }
}
