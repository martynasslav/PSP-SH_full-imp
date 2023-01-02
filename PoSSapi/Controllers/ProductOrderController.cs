using System.ComponentModel.DataAnnotations;
using Classes;
using PoSSapi.Classes;
using Enums;
using Microsoft.AspNetCore.Mvc;
using PoSSapi.Tools;
using PoSSapi.Repositories;
using PoSSapi.Dtos;

namespace PoSSapi.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductOrderController : ControllerBase
{
    private readonly IProductOrderRepository _productOrderRepository;
    private readonly IOrderProductRepository _orderProductRepository;

    public ProductOrderController(IProductOrderRepository productOrderRepository, IOrderProductRepository orderProductRepository)
    {
        _productOrderRepository = productOrderRepository;
        _orderProductRepository = orderProductRepository;
    }

    protected class OrderProductReturnObject
    {
        public int totalItems { get; set; }
        public Shift[] itemList { get; set; }
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet(Name = "GetAllProductOrders")]
    public ActionResult GetAllProductOrders([FromQuery] string? customerId, [FromQuery] OrderStatusState? status,
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

        var productOrders = _productOrderRepository.GetAllProductOrders();

        if (customerId != null)
        {
            productOrders = productOrders.Where(p => p.CustomerId == customerId);
        }

        if (status != null)
        {
            productOrders = productOrders.Where(p => p.OrderStatus == status);
        }

        productOrders = productOrders.Skip (pageNum * itemsPerPage).Take(itemsPerPage);

        return Ok(productOrders);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductOrder))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}", Name = "GetProductOrder")]
    public ActionResult<ProductOrder> GetProductOrder(string id)
    {
        var productOrder = _productOrderRepository.GetProductOrder(id);

        if (productOrder == null)
        {
            return NoContent();
        }

        return productOrder;
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
        var productOrder = _productOrderRepository.GetProductOrder(id);
        
        if (productOrder == null)
        {
            return NotFound();
        }

        productOrder.OrderStatus = status;

        _productOrderRepository.UpdateProductOrder(productOrder);

        return Ok();
    }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("CreateProductOrder")]
    public ActionResult PostProductOrder(CreateProductOrderDto newProductOrder)
    {
        var productOrder = new ProductOrder()
        {
            Id = Guid.NewGuid().ToString(),
            StartDate = newProductOrder.StartDate,
            FinishDate = newProductOrder.FinishDate,
            CustomerId = newProductOrder.CustomerId,
            EmployeeId = newProductOrder.EmployeeId,
            Payments = newProductOrder.Payments,
            TableNumber = newProductOrder.TableNumber,
            Tips = newProductOrder.Tips,
            OrderType = newProductOrder.OrderType
        };

        _productOrderRepository.CreateProductOrder(productOrder);

        return CreatedAtAction("GetAllProductOrders", new { id = productOrder.Id}, productOrder);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("{id}", Name = "UpdateProductOrder")]
    public ActionResult<ProductOrder> UpdateOrder(string id, ProductOrder productOrder)
    {
        var _productOrder = _productOrderRepository.GetProductOrder(id);

        if (_productOrder == null)
        {
            return NotFound();
        }

        productOrder.Id = _productOrder.Id;

        _productOrderRepository.UpdateProductOrder(productOrder);

        return Ok(productOrder);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("{id}", Name = "DeleteProductOrder")]
    public ActionResult<ProductOrder> DeleteProductOrder(string id)
    {
        var productOrder = _productOrderRepository.GetProductOrder(id);

        if (productOrder == null)
        {
            return NotFound();
        }

        _productOrderRepository.DeleteProductOrder(productOrder);

        return NoContent();
    }

    /** <summary>Get order products of an existing order</summary>
     * <param name="id">Id of the product order that you want to get products of</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderProductReturnObject[]))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id}/orderProducts")]
    public ActionResult GetOrderProducts(string id)
    {
        var productOrder = _productOrderRepository.GetProductOrder(id);
        var orderProducts = _orderProductRepository.GetAllOrderProducts();
        
        orderProducts = orderProducts.Where(o => o.TableNumber == productOrder.TableNumber);

        return Ok(orderProducts);
    }
    
    /** <summary>Add order products to an existing order</summary>
     * <param name="id">Id of the product order that you want to add products to</param>
     */
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("{id}/orderProducts")]
    public ActionResult PostOrderProduct(string id, [FromBody] OrderProduct newOrderProduct)
    {
        var productOrder = _productOrderRepository.GetProductOrder(id);

        var orderProduct = new OrderProduct()
        {
            Id = Guid.NewGuid().ToString(),
            TableNumber = productOrder.TableNumber,
            ProductId = newOrderProduct.ProductId,
            Quantity = newOrderProduct.Quantity,
            Details = newOrderProduct.Details,
            OrderId = productOrder.Id
        };

        _orderProductRepository.CreateOrderProduct(orderProduct);

        return CreatedAtAction("GetOrderProducts", new { id = orderProduct.Id}, orderProduct);
    }
    
    /** <summary>Edit order products in an existing order</summary>
     * <param name="id">Id of the order product that you want to edit</param>
     */
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut("orderProducts/{id}/put")]
    public ActionResult<OrderProduct> PutOrderProduct(string id, OrderProduct orderProduct)
    {
        var _orderProduct = _orderProductRepository.GetOrderProduct(id);

        if (_orderProduct == null)
        {
            return NotFound();
        }

        orderProduct.Id = _orderProduct.Id;

        _orderProductRepository.UpdateOrderProduct(orderProduct);

        return Ok(orderProduct);
    }
    
    /** <summary>Remove an order product from an existing order</summary>
     * <param name="id">Id of the order product that you want to remove</param>
     */
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpDelete("orderProducts/{id}/delete")]
    public ActionResult DeleteOrderProduct(string id)
    {
        var orderProduct = _orderProductRepository.GetOrderProduct(id);

        if (orderProduct == null)
        {
            return NotFound();
        }

        _orderProductRepository.DeleteOrderProduct(orderProduct);

        return NoContent();
    }

}
