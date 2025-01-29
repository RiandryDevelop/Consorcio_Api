using Consorcio_Api.Application.Interfaces.IFileStore;

namespace Consorcio_Api.Application.Services.LocalFileStoreService
{
    public class LocalFileStoreService : IFileStore
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalFileStoreService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> Store(string container, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var nombrefile = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string ruta = Path.Combine(folder, nombrefile);
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                var contenido = ms.ToArray();
                await File.WriteAllBytesAsync(ruta, contenido);
            }

            var request = httpContextAccessor.HttpContext!.Request!;

            var url = $"{request.Scheme}://{request.Host}";
            var urlfile = Path.Combine(url, container, nombrefile).Replace("\\", "/");
            return urlfile;
        }

        public Task Remove(string? ruta, string container)
        {
            if (string.IsNullOrWhiteSpace(ruta))
            {
                return Task.CompletedTask;
            }

            var nombrefile = Path.GetFileName(ruta);
            var directoriofile = Path.Combine(env.WebRootPath, container, nombrefile);

            if (File.Exists(directoriofile))
            {
                File.Delete(directoriofile);
            }

            return Task.CompletedTask;

        }
    }

}
