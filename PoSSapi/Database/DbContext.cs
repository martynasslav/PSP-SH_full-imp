using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Classes;

namespace PoSSapi.Database
{
	internal class DbEntities : DbContext
	{
	    public string DbPath { get; }

        public DbEntities()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            DbPath = Path.Join(path, "data.db");
		}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }

        public DbSet<Client> Clients { get; set; }
    }
}