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
        public DbSet<Product> Products { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<OrderService> OrderServices { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}