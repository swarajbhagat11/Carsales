using Carsales.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Carsales.Infrastructure
{
    public class SalesContext : DbContext
    {
        public DbSet<CarDTO> Car { get; set; }

        public SalesContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Sales.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }
    }
}
