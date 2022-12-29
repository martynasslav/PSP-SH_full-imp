using Classes;
using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbEntities _dbEntities;

        public CustomerRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _dbEntities.Customers.AsEnumerable();
        }

        public Customer? GetCustomerById(string id)
        {
            return _dbEntities.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void InsertCustomer(Customer customer)
        {
            _dbEntities.Customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var item = _dbEntities.Customers.First(x => x.Id == customer.Id);
            item.Name = customer.Name;
            item.Surname = customer.Surname;
            item.Birthday = customer.Birthday;
            item.Address = customer.Address;
            item.CardNumber = customer.CardNumber; 
        }
        
        public void DeleteCustomer(Customer customer)
        {
            _dbEntities.Customers.Remove(customer);
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}
