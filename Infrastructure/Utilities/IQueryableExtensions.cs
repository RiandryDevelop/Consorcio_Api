using Consorcio_Api.Application.DTOs;

namespace Consorcio_Api.Infrastructure.Utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, PaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsByPage)
                .Take(pagination.RecordsByPage);
        }
    }
}
