using Consorcio_Api.Models;

namespace Consorcio_Api.Services.Contract
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetList();
    }
}
