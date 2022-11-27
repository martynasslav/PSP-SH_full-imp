using System.ComponentModel.DataAnnotations;
using Classes;
using Enums;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductOrderController : GenericController<ProductOrder>
{
    protected class OrderProductReturnObject
    {
        public int totalItems { get; set; }
        public Shift[] itemList { get; set; }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnObject))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAll([FromQuery] string? locationId, [FromQuery] OrderStatusState? status,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0 || pageNum < 0)
        {
            return BadRequest("Invalid itemsPerPage or pageNum");
        }
        
        int totalItems = 20;  
        int itemsToDisplay = ControllerTools.calculateItemsToDisplay(itemsPerPage, pageNum, totalItems);

        var objectList = new ProductOrder[itemsToDisplay];
        for (var i = 0; i < itemsToDisplay; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<ProductOrder>();
            objectList[i].OrderStatus = status ?? OrderStatusState.New;
        }

        ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
        return Ok(returnObject);
    }

    /** <summary>Changes status of a certain product order</summary>
     * <param name="id" example="">Id of the product order that you want the status changed</param>
     * <param name="status" example="">Status that you want the product order to be in</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public ActionResult ChangeStatus(string id, [FromQuery][Required] OrderStatusState status)
    {
        var productOrder = RandomGenerator.GenerateRandom<ProductOrder>(id);
        productOrder.OrderStatus = status;

        return Ok();
    }
    
    /** <summary>Get order products of an existing order</summary>
     * <param name="id">Id of the product order that you want to get products of</param>
     * <param name="itemsPerPage">Number of order products returned in the response</param>
     * <param name="pageNum">Number of the chunk of order products returned in the response</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderProductReturnObject[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orderProducts")]
    public ActionResult GetOrderProducts(string id, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        return Ok();
    }
    
    /** <summary>Add order products to an existing order</summary>
     * <param name="id">Id of the product order that you want to add products to</param>
     * <param name="orderProducts">Order product list in body to add to the product order</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/orderProducts")]
    public ActionResult PostOrderProducts(string id, [FromBody] OrderProduct[] orderProducts)
    {
        return Ok();
    }
    
    /** <summary>Edit order products in an existing order</summary>
     * <param name="id">Id of the product order that you want to edit an order product in</param>
     * <param name="orderProducts">Order products in body to edit in the product order</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}/orderProducts")]
    public ActionResult PutOrderProducts(string id, [FromBody] OrderProduct[] orderProducts)
    {
        return Ok();
    }
    
    /** <summary>Remove an order product from an existing order</summary>
     * <param name="id">Id of the product order that you want to remove a product from</param>
     * <param name="orderProductId">Id of the order product to remove from the product order</param>
     */
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}/orderProducts/{orderProductId}")]
    public ActionResult DeleteOrderProduct(string id, string orderProductId)
    {
        return Ok();
    }

}
