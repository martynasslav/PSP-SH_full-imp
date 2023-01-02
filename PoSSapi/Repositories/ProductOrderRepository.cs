using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly DbEntities _dbEntities;

        public ProductOrderRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<ProductOrder> GetAllProductOrders()
        {
            return _dbEntities.ProductOrders;
        }

        public ProductOrder GetProductOrder(string id)
        {
            return _dbEntities.ProductOrders.Find(id);
        }

        public void CreateProductOrder(ProductOrder productOrder)
        {
            _dbEntities.ProductOrders.Add(productOrder);
            _dbEntities.SaveChanges();
        }

        public void UpdateProductOrder(ProductOrder productOrder)
        {
            var _productOrder = _dbEntities.ProductOrders.Find(productOrder.Id);
            _productOrder.Id = productOrder.Id;
            _productOrder.OrderStatus = productOrder.OrderStatus;
            _productOrder.StartDate = productOrder.StartDate;
            _productOrder.FinishDate = productOrder.FinishDate;
            _productOrder.CustomerId = productOrder.CustomerId;
            _productOrder.EmployeeId = productOrder.EmployeeId;
            _productOrder.Payments = productOrder.Payments;
            _productOrder.TableNumber = productOrder.TableNumber;
            _productOrder.Tips = productOrder.Tips;
            _productOrder.OrderType = productOrder.OrderType;
            _dbEntities.ProductOrders.Update(_productOrder);
            _dbEntities.SaveChanges();
        }

        public void DeleteProductOrder(ProductOrder productOrder)
        {
            _dbEntities.ProductOrders.Remove(productOrder);
            _dbEntities.SaveChanges();
        }
    }
}
