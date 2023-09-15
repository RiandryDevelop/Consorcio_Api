using Microsoft.EntityFrameworkCore;
using Consorcio_Api.Models;
using Consorcio_Api.Services.Contract;

namespace Consorcio_Api.Services.Implementation
{
    public class DepartmentService: IDepartmentService
    {
        private readonly DbemployeesContext _dbContext;

        public DepartmentService(DbemployeesContext dbContext)
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

            }catch (Exception ex) {
                throw new Exception("My Custom Error Message", ex);
            }
        }
    }
}
      