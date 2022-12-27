using Classes;

namespace PoSSapi.Repository
{
    public interface IServiceRepository
    {
        void CreateService(Service service);
        Service GetService(string id);
        IEnumerable<Service> GetAllServices();
        void UpdateService(Service service);
        void DeleteService(Service service);
    }
}
