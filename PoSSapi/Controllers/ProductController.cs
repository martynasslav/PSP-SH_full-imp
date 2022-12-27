using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Database;
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

    [ProducesResponseType(200)]
    [HttpGet(Name = "GetAllProducts")]
    public IEnumerable<Product> GetAllProducts([FromQuery] string? locationId, [FromQuery] string? categoryId, 
        [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
    {
        var products = _productRepository.GetAllProducts();

        if (locationId != null)
        {
            products = products.Where(p => p.LocationId == locationId);
        }

        if (categoryId != null)
        {
            products = products.Where(p => p.CategoryId == categoryId);
        }

        return products.Skip(pageNum).Take(itemsPerPage);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [HttpGet("{id}", Name ="GetProduct")]
    public ActionResult<Product> GetProduct(string id) 
    {
        var product = _productRepository.GetProduct(id);

        if (product == null)
        {
            return NoContent();
        }

        return product;
    }

    [ProducesResponseType(201)]
    [HttpPost(Name = "CreateProduct")]
    public ActionResult<Product> CreateProduct(Product product) 
    {
        _productRepository.CreateProduct(product);
        return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpPut("{id}", Name ="UpdateProduct")]
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

    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [HttpDelete("{id}", Name ="DeleteProduct")]
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