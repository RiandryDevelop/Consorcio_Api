using Consorcio_Api.Domain.Models;

namespace Consorcio_Api.Application.Interfaces
{
    public interface IEmployee
    {
        Task<List<Employee>> GetList();
        Task<Employee> Get(int IdEmployee);

        Task<Employee> Add(Employee model);

        Task<bool> Update(Employee model);

        Task<bool> Delete(Employee model);

    }
}
