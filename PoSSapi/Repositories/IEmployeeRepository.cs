using PoSSapi.Classes;

namespace PoSSapi.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee? GetEmployeeById(string id);
        void InsertEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void Save();
    }
}