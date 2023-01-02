using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbEntities _dbEntities;

        public OrderRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbEntities.Orders;
        }

        public Order GetOrder(string id)
        {
            return _dbEntities.Orders.Find(id);
        }

        public void CreateOrder(Order order)
        {
            _dbEntities.Orders.Add(order);
            _dbEntities.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            var _order = _dbEntities.Orders.Find(order.Id);
            _order.Id = order.Id;
            _order.OrderStatus = order.OrderStatus;
            _order.StartDate = order.StartDate;
            _order.FinishDate = order.FinishDate;
            _order.CustomerId = order.CustomerId;
            _order.EmployeeId = order.EmployeeId;
            _order.Payments = order.Payments;
            _dbEntities.Orders.Update(_order);
            _dbEntities.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _dbEntities.Orders.Remove(order);
            _dbEntities.SaveChanges();
        }
    }
}
