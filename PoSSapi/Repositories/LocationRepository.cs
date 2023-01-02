using Classes;
using PoSSapi.Classes;
using PoSSapi.Database;

namespace PoSSapi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly DbEntities _dbEntities;

        public LocationRepository(DbEntities dbEntities)
        {
            _dbEntities = dbEntities;
        }

        public void InsertLocation(Location location)
        {
            _dbEntities.Locations.Add(location);
        }

        public Location? GetLocationById(string id)
        {
            return _dbEntities.Locations.FirstOrDefault(l => l.Id == id);
        }

        public IEnumerable<Location> GetLocations()
        {
            return _dbEntities.Locations.AsEnumerable();
        }

        public void UpdateLocation(Location location)
        {
            var item = _dbEntities.Locations.First(l => l.Id == location.Id);
            item.Address = location.Address;
            item.Name = location.Name;
        }

        public void DeleteLocation(Location location)
        {
            _dbEntities.Locations.Remove(location);
        }

        public void Save()
        {
            _dbEntities.SaveChanges();
        }
    }
}