namespace Consorcio_Api.Application.Interfaces.IFileStore
{
    public interface IFileStore
    {
        Task<string> Store(string container, IFormFile file);
        Task Remove(string? route, string container);
        async Task<string> Edit(string? route, string container, IFormFile file)
        {
            await Remove(route, container);
            return await Store(container, file);
        }
    }
}
