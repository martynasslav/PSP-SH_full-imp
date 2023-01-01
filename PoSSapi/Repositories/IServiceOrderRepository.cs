using Classes;

namespace PoSSapi.Repositories
{
    public interface IServiceOrderRepository
    {
        IEnumerable<OrderService> GetAllServiceOrders();
        OrderService GetServiceOrder(string id);
        void CreateServiceOrder(OrderService orderService);
        void UpdateServiceOrder(OrderService orderService);
        void DeleteServiceOrder(OrderService orderService);
    }
}
