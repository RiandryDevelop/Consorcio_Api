using Consorcio_Api.Models; 

namespace Consorcio_Api.Services.Contract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetList();
        Task<Employee> Get(int IdEmployee);
       
        Task<Employee> Add(Employee model);

        Task<bool> Update(Employee model);

        Task<bool> Delete(Employee model);

    }
}
