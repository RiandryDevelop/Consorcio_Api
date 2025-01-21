using Consorcio_Api.Domain.Models;

namespace Consorcio_Api.Application.Interfaces
{
    public interface IDepartment
    {
        Task<List<Department>> GetList();
    }
}
