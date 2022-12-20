using Classes;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Database;
using PoSSapi.Repository;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : GenericController<Product>
{
    private IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
       _productRepository= productRepository;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllProducts")]
    public ActionResult GetAllProducts([FromQuery] string? locationId, [FromQuery] string? categoryId, 
        [FromQuery] int itemsPerPage=10, [FromQuery] int pageNum=0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }
        
        int totalItems = 20;  
        int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

        var objectList = new Product[itemsToDisplay];
        for (var i = 0; i < itemsToDisplay; i++)
        {
            objectList[i] = (Product)_productRepository.GetAllProducts();
            
            if (locationId != null)
            {
                objectList[i].LocationId = locationId;
            }
            
            if (categoryId != null)
            {
                objectList[i].CategoryId = categoryId;
            }
        }
        
        ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
        return Ok(returnObject);
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
        _productRepository.UpdateProducts(product);
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