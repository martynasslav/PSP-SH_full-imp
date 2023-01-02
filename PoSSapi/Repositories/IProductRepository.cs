﻿using Classes;

namespace PoSSapi.Repositories
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        Product GetProduct(string id);
        IEnumerable<Product> GetAllProducts();
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
