using Classes;
using PoSSapi.Database;

namespace PoSSapi.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly DbEntities _dbEntities;

        public ServiceRepository(DbEntities dbEntities) 
        {
            _dbEntities = dbEntities;
        }

        public void CreateService(Service service)
        {
            _dbEntities.Services.Add(service);
            _dbEntities.SaveChanges();
        }

        public void DeleteService(Service service)
        {
            _dbEntities.Services.Remove(service);   
            _dbEntities.SaveChanges();
        }

        public IEnumerable<Service> GetAllServices()
        {
            return _dbEntities.Services;
        }

        public Service GetService(string id)
        {
            return _dbEntities.Services.Find(id);
        }

        public void UpdateService(Service service)
        {
            var _service = _dbEntities.Services.Find(service.Id);
            _service.Id = service.Id;
            _service.Name = service.Name;
            _service.EmployeeId = service.EmployeeId;
            _service.Price = service.Price;
            _service.Duration = service.Duration;
            _service.CategoryId = service.CategoryId;
            _service.LocationId = service.LocationId;
            _dbEntities.Update(_service);
            _dbEntities.SaveChanges();
        }
    }
}
