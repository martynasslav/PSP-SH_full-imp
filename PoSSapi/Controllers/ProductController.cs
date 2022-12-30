using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Database;
using PoSSapi.Dtos;
using PoSSapi.Repository;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
       _productRepository= productRepository;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllProducts")]
    public ActionResult GetAllProducts([FromQuery] string? locationId, [FromQuery] string? categoryId, 
        [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
    {
        if (itemsPerPage <= 0)
            return BadRequest("itemsPerPage must be greater than 0");

        if (pageNum < 0)
            return BadRequest("pageNum must be 0 or greater");

        var products = _productRepository.GetAllProducts();

        if (locationId != null)
        {
            products = products.Where(p => p.LocationId == locationId);
        }

        if (categoryId != null)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        products = products.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(products);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetProduct")]
    public ActionResult<Product> GetProduct(string id) 
    {
        var product = _productRepository.GetProduct(id);

        if (product == null)
        {
            return NoContent();
        }

        return product;
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost(Name = "CreateProduct")]
    public ActionResult<Product> CreateProduct(CreateProductDto newProduct) 
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = newProduct.Name,
            Price = newProduct.Price,
            Tax = newProduct.Tax,
            CategoryId = newProduct.CategoryId,
            LocationId = newProduct.LocationId
        };

        _productRepository.CreateProduct(product);

        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}", Name = "UpdateProduct")]
    public ActionResult<Product> UpdateProduct(string id, Product product) 
    {
        var _product = _productRepository.GetProduct(id);

        if (_product == null)
        {
            return NotFound();
        }

        product.Id = _product.Id;

        _productRepository.UpdateProduct(product);

        return Ok(product);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}", Name = "DeleteProduct")]
    public ActionResult<Product> DeleteProduct(string id) 
    {
        var product = _productRepository.GetProduct(id);
        
        if (product == null)
        {
            return NotFound();
        }

        _productRepository.DeleteProduct(product);

        return Ok();
    }
}