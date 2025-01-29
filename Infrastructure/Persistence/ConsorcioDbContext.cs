using Microsoft.EntityFrameworkCore;
using Consorcio_Api.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Consorcio_Api.Infrastructure.Persistence
{
    public class ConsorcioDbContext : IdentityDbContext
    {
        public ConsorcioDbContext(DbContextOptions<ConsorcioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
