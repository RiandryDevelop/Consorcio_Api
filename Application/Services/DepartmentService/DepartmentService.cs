using Microsoft.EntityFrameworkCore;
using Consorcio_Api.Application.Interfaces;
using Consorcio_Api.Domain.Models;
using Consorcio_Api.Infrastructure.Persistence;

namespace Consorcio_Api.Application.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly ConsorcioDbContext _dbContext;

        public DepartmentService(ConsorcioDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Department>> GetList()
        {
            try
            {
                List<Department> list = new();
                list = await _dbContext.Departments.ToListAsync();
                return list;

            }
            catch (Exception ex)
            {
                throw new Exception("My Custom Error Message", ex);
            }
        }
    }
}
