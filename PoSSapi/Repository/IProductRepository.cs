using Classes;

namespace PoSSapi.Repository
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(string id);
        IEnumerable<Product> GetAllProducts();
        void UpdateProducts(Product product);
        void DeleteProduct(Product product);
    }
}
