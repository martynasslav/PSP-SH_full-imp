using Classes;

namespace PoSSapi.Repositories
{
    public interface IProductOrderRepository
    {
        IEnumerable<ProductOrder> GetAllProductOrders();
        ProductOrder GetProductOrder(string id);
        void CreateProductOrder(ProductOrder productOrder);
        void UpdateProductOrder(ProductOrder productOrder);
        void DeleteProductOrder(ProductOrder productOrder);
    }
}
