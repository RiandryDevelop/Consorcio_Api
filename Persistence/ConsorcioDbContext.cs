using Microsoft.EntityFrameworkCore;
using Consorcio_Api.Domain.Models;


namespace Consorcio_Api.Persistence
{
    public class ConsorcioDbContext : DbContext
    {
        public ConsorcioDbContext(DbContextOptions<ConsorcioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
