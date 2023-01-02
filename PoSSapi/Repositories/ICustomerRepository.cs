using Classes;

namespace PoSSapi.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Customer? GetCustomerById(string id);
        void InsertCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(Customer customer);
        void Save();
    }
}
