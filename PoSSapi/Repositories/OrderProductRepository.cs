using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly DbEntities _dbEntities;

        public OrderProductRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<OrderProduct> GetAllOrderProducts()
        {
            return _dbEntities.OrderProducts;
        }

        public OrderProduct GetOrderProduct(string id)
        {
            return _dbEntities.OrderProducts.Find(id);
        }

        public void CreateOrderProduct(OrderProduct orderProduct)
        {
            _dbEntities.OrderProducts.Add(orderProduct);
            _dbEntities.SaveChanges();
        }

        public void UpdateOrderProduct(OrderProduct orderProduct)
        {
            var _orderProduct = _dbEntities.OrderProducts.Find(orderProduct.Id);
            _orderProduct.Id = orderProduct.Id;
            _orderProduct.TableNumber = orderProduct.TableNumber;
            _orderProduct.ProductId = orderProduct.ProductId;
            _orderProduct.Quantity = orderProduct.Quantity;
            _orderProduct.Details = orderProduct.Details;
            _orderProduct.OrderId = orderProduct.OrderId;
            _dbEntities.OrderProducts.Update(_orderProduct);
            _dbEntities.SaveChanges();
        }

        public void DeleteOrderProduct(OrderProduct orderProduct)
        {
            _dbEntities.OrderProducts.Remove(orderProduct);
            _dbEntities.SaveChanges();
        }
    }
}
