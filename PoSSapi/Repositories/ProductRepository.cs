using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbEntities _dbEntities;

        public ProductRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public void CreateProduct(Product product)
        {
            _dbEntities.Products.Add(product);
            _dbEntities.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _dbEntities.Products.Remove(product);
            _dbEntities.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbEntities.Products;
        }

        public Product GetProduct(string id)
        {
            return _dbEntities.Products.Find(id);
        }

        public void UpdateProduct(Product product)
        {
            var _product = _dbEntities.Products.Find(product.Id);
            _product.Id = product.Id;
            _product.Name = product.Name;
            _product.Price = product.Price;
            _product.Tax= product.Tax;
            _product.CategoryId= product.CategoryId;
            _product.LocationId = product.LocationId;
            _dbEntities.Products.Update(_product);
            _dbEntities.SaveChanges();
        }
    }
}
