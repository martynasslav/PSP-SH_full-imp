using System.ComponentModel.DataAnnotations;
using Classes;
using PoSSapi.Classes;
using Enums;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceOrderController : GenericController<Order>
{
    protected class OrderServiceReturnObject
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

        var objectList = new Order[itemsToDisplay];
        for (var i = 0; i < itemsToDisplay; i++)
        {
            objectList[i] = RandomGenerator.GenerateRandom<Order>();
            objectList[i].OrderStatus = status ?? OrderStatusState.New;
        }

        ReturnObject returnObject = new ReturnObject {totalItems = totalItems, itemList = objectList};
        return Ok(returnObject);
    }

    /** <summary>Changes status of a certain service order</summary>
     * <param name="id" example="">Id of the service order that you want the status changed</param>
     * <param name="status" example="">Status that you want the service order to be in</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPatch("{id}")]
    public ActionResult ChangeStatus(string id, [FromQuery][Required] OrderStatusState status)
    {
        var serviceOrder = RandomGenerator.GenerateRandom<Order>(id);
        serviceOrder.OrderStatus = status;

        return Ok();
    }
    
    /** <summary>Get order services of an existing order</summary>
     * <param name="id">Id of the service order that you want to get services of</param>
     * <param name="itemsPerPage">Number of order services returned in the response</param>
     * <param name="pageNum">Number of the chunk of order services returned in the response</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderServiceReturnObject[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orderServices")]
    public ActionResult GetOrderServices(string id, [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        return Ok();
    }
    
    /** <summary>Add order services to an existing order</summary>
     * <param name="id">Id of the service order that you want to add services to</param>
     * <param name="orderServices">Order service list in body to add to the service order</param>
     */
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/orderServices")]
    public ActionResult PostOrderServices(string id, [FromBody] OrderService[] orderServices)
    {
        return Ok();
    }
    
    /** <summary>Edit order services in an existing order</summary>
     * <param name="id">Id of the service order that you want to edit an order service in</param>
     * <param name="orderServices">Order services in body to edit in the service order</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}/orderServices")]
    public ActionResult PutOrderServices(string id, [FromBody] OrderService[] orderServices)
    {
        return Ok();
    }
    
    /** <summary>Remove an order service from an existing order</summary>
     * <param name="id">Id of the service order that you want to remove a service from</param>
     * <param name="orderServiceId">Id of the order service to remove from the service order</param>
     */
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}/orderServices/{orderServiceId}")]
    public ActionResult DeleteOrderService(string id, string orderServiceId)
    {
        return Ok();
    }
}
