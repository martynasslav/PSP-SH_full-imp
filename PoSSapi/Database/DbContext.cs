using Classes;
using Microsoft.EntityFrameworkCore;
using PoSSapi.Classes;

namespace PoSSapi.Database
{
    public class DbEntities : DbContext
	{
        public DbEntities(DbContextOptions<DbEntities> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}