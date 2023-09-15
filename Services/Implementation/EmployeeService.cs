using Microsoft.EntityFrameworkCore;
using Consorcio_Api.Models;
using Consorcio_Api.Services.Contract;

namespace Consorcio_Api.Services.Implementation
{
    public class EmployeeService: IEmployeeService
    {
        private readonly DbemployeesContext _dbContext;
       // private readonly object IdDepartmentNavigation;

        public EmployeeService(DbemployeesContext dbContext)
        {
            _dbContext = dbContext;
        }
        // CRUD METHODS - C
        public async Task<Employee> Add(Employee model)
        {
            try {
                _dbContext.Employees.Add(model);
                await _dbContext.SaveChangesAsync();
                return model;

            } catch (Exception) { 
                throw;
            }
        }
        // CRUD METHODS - R
        public async Task<Employee> Get(int IdEmployee)
        {
            try {
                Employee? foundONe = new ();
                return foundONe = await _dbContext.Employees.Include(dpt => dpt.IdDepartmentNavigation).
                    Where(e => e.IdEmployee == IdEmployee).FirstOrDefaultAsync();

            } catch (Exception ex) {
                throw ex;
            }
        }
        // CRUD METHODS - U
        public async Task<bool> Update(Employee model)
        {
            try {
                _dbContext.Employees.Update(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                throw;
            }
        }
        // CRUD METHODS - D
        public async Task<bool> Delete(Employee model)
        {
            try {
                _dbContext.Employees.Remove(model);
                await _dbContext.SaveChangesAsync();
                return true;

            }
            catch (Exception) { 
                throw; 
            }
        }
        public async Task<List<Employee>> GetList()
        {
            try { 
            List<Employee> list = new();
            list = await _dbContext.Employees.Include(dpt => dpt.IdDepartmentNavigation).ToListAsync();
                return list;
            } catch (Exception) { 
                throw;
            }
        }

    }
}
