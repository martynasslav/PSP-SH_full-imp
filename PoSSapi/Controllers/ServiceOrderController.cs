using System.ComponentModel.DataAnnotations;
using Dtos;
using Enums;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;
using Classes;
using PoSSapi.Classes;
using PoSSapi.Repositories;
using PoSSapi.Dtos;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ServiceOrderController : ControllerBase
{
    private IOrderRepository _orderRepository;
    private IServiceOrderRepository _serviceOrderRepository;

    public ServiceOrderController(IOrderRepository orderRepository, IServiceOrderRepository serviceOrderRepository)
    {
        _orderRepository = orderRepository;
        _serviceOrderRepository = serviceOrderRepository;
    }

    protected class OrderServiceReturnObject
    {
        public int totalItems { get; set; }
        public Shift[] itemList { get; set; }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet]
    public ActionResult GetAllServiceOrders([FromQuery] string? customerId, [FromQuery] OrderStatusState? status,
        [FromQuery] int itemsPerPage = 10, [FromQuery] int pageNum = 0)
    {
        if (itemsPerPage <= 0)
        {
            return BadRequest("itemsPerPage must be greater than 0");
        }
        if (pageNum < 0)
        {
            return BadRequest("pageNum must be 0 or greater");
        }

        var orders = _orderRepository.GetAllOrders();

        if (customerId != null)
        {
            orders = orders.Where(o => o.CustomerId == customerId);
        }

        if (status != null)
        {
            orders = orders.Where(o => o.OrderStatus == status);
        }

        orders = orders.Skip(pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(orders);
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
        var order = _orderRepository.GetOrder(id);

        if (order == null)
        {
            return NotFound();
        }

        order.OrderStatus = status;

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("CreateServiceOrder")]
    public ActionResult PostServiceOrder(CreateOrderDto newOrder)
    {
        var order = new Order()
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = newOrder.StartDate,
            FinishDate = newOrder.FinishDate,
            CustomerId = newOrder.CustomerId,
            EmployeeId = newOrder.EmployeeId,
            Payments = newOrder.Payments
        };

        _orderRepository.CreateOrder(order);

        return CreatedAtAction("GetAllServiceOrders", new { id = order.Id }, order);
    }

    /** <summary>Get order services of an existing order</summary>
     * <param name="id">Id of the service order that you want to get services of</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderServiceReturnObject[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orderServices")]
    public ActionResult GetOrderServices(string id)
    {
        var orderService = _serviceOrderRepository.GetAllServiceOrders();

        orderService = orderService.Where(o => o.OrderId == id);

        return Ok(orderService);
    }
    
    /** <summary>Add order services to an existing order</summary>
     * <param name="id">Id of the service order that you want to add services to</param>
     */
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/orderServices")]
    public ActionResult PostOrderServices(string id, [FromBody] OrderService newOrderService)
    {
        var orderService = new OrderService()
        {
            Id = Guid.NewGuid().ToString(),
            ServiceId = newOrderService.ServiceId,
            Quantity = newOrderService.Quantity,
            Details = newOrderService.Details,
            OrderId = id
        };

        _serviceOrderRepository.CreateServiceOrder(orderService);

        return CreatedAtAction("GetOrderServices", new { id = orderService.Id }, orderService);
    }
    
    /** <summary>Edit order services in an existing order</summary>
     * <param name="id">Id of the order service that you want to edit</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}/orderServices")]
    public ActionResult<OrderService> PutOrderServices(string id, OrderService orderService)
    {
        var _orderService = _serviceOrderRepository.GetServiceOrder(id);

        if (_orderService == null)
        {
            return NotFound();
        }

        orderService.Id = _orderService.Id;

        _serviceOrderRepository.UpdateServiceOrder(orderService);

        return Ok(orderService);
    }
    
    /** <summary>Remove an order service from an existing order</summary>
     * <param name="id">Id of the order service that you want to remove</param>
     */
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}/orderServices/{orderServiceId}")]
    public ActionResult DeleteOrderService(string id)
    {
        var orderService = _serviceOrderRepository.GetServiceOrder(id);

        if (orderService == null)
        {
            return NotFound();
        }

        _serviceOrderRepository.DeleteServiceOrder(orderService);

        return NoContent();
    }

    /** <summary>Gets amount of orders for a service in a specified period, if a period isnt specified returns all available order count</summary>
     * <param name="id" example="">Id of the service</param>
     * <param name="startDate" example="">Period start date</param>
     * <param name="endDate" example="">Period end date</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orders")]
    public ActionResult GetServiceStatistics(string id, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        var listSize = new Random().Next(1, 10);

        var resultList = new List<ServiceStatisticDto>(listSize);

        for (int i = 0; i < listSize; i++)
        {
            var randomStatistic = RandomGenerator.GenerateRandom<ServiceStatisticDto>();
            randomStatistic.ServiceId = id;
            resultList.Add(randomStatistic);
        }

        return Ok(resultList);
    }
}
