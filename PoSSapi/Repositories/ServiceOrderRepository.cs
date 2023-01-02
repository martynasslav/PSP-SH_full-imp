using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private readonly DbEntities _dbEntities;

        public ServiceOrderRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<OrderService> GetAllServiceOrders()
        {
            return _dbEntities.OrderServices;
        }

        public OrderService GetServiceOrder(string id)
        {
            return _dbEntities.OrderServices.Find(id);
        }

        public void CreateServiceOrder(OrderService orderService)
        {
            _dbEntities.OrderServices.Add(orderService);
            _dbEntities.SaveChanges();
        }

        public void UpdateServiceOrder(OrderService orderService)
        {
            var _orderService = _dbEntities.OrderServices.Find(orderService.Id);
            _orderService.Id = orderService.Id;
            _orderService.ServiceId = orderService.ServiceId;
            _orderService.Quantity = orderService.Quantity;
            _orderService.Details = orderService.Details;
            _orderService.OrderId = orderService.OrderId;
            _dbEntities.OrderServices.Update(_orderService);
            _dbEntities.SaveChanges();
        }

        public void DeleteServiceOrder(OrderService orderService)
        {
            _dbEntities.OrderServices.Remove(orderService);
            _dbEntities.SaveChanges();
        }
    }
}
