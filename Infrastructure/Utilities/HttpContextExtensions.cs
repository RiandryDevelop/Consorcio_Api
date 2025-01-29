using Microsoft.EntityFrameworkCore;

namespace Consorcio_Api.Infrastructure.Utilities
{
    public static class HttpContextExtensions
    {
        public async static Task InsertParametersPaginationInHeader<T>(this HttpContext httpContext,
           IQueryable<T> queryable)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            double quantity = await queryable.CountAsync();
            httpContext.Response.Headers.Append("total-quantity-records", quantity.ToString());
        }
    }
}
