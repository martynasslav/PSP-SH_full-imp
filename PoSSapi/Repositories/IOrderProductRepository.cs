using Classes;

namespace PoSSapi.Repositories
{
    public interface IOrderProductRepository
    {
        IEnumerable<OrderProduct> GetAllOrderProducts();
        OrderProduct GetOrderProduct(string id);
        void CreateOrderProduct(OrderProduct orderProduct);
        void UpdateOrderProduct(OrderProduct orderProduct);
        void DeleteOrderProduct(OrderProduct orderProduct);
    }
}
