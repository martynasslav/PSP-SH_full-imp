using Classes;
using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DbEntities _dbEntities;

        public EmployeeRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _dbEntities.Employees.AsEnumerable();
        }

        public Employee? GetEmployeeById(string id)
        {
            return _dbEntities.Employees.FirstOrDefault(e => e.Id == id);
        }

        public void InsertEmployee(Employee employee)
        {
            _dbEntities.Employees.Add(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            _dbEntities.Employees.Remove(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            var item = _dbEntities.Employees.First(e => e.Id == employee.Id);
            item.Name = employee.Name;
            item.Email = employee.Email;
            item.Phone = employee.Phone;
            item.Address = employee.Address;
            item.LocationId = employee.LocationId;
            item.IsManager = employee.IsManager;
            item.MonthlyWorkHours = employee.MonthlyWorkHours;
            item.Password = employee.Password;
            item.Username = employee.Username;
            item.Birthday = employee.Birthday;
            item.Surname = employee.Surname;
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}