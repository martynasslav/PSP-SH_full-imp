using Classes;
using PoSSapi.Classes;

namespace PoSSapi.Repositories
{
    public interface ILocationRepository
    {
        IEnumerable<Location> GetLocations();
        Location? GetLocationById(string id);
        void InsertLocation(Location location);
        void DeleteLocation(Location location);
        void UpdateLocation(Location location);
        void Save();
    }
}