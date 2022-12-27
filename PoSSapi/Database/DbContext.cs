using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Classes;

namespace PoSSapi.Database
{
	public class DbEntities : DbContext
    { 
        public DbEntities(DbContextOptions<DbEntities> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
    }
}