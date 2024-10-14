using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LibraryRent.Repositories.Utils
{
    public static class HttpContextExtension
    {

        public async static Task InsertPaginationHeader<T>(this HttpContext httpContext, IQueryable<T> queryable)
        {
            if (httpContext is null)
                throw new ArgumentNullException(nameof(httpContext));

            double totalRecords = await queryable.CountAsync();
            httpContext.Response.Headers.Add("TotalRegistros", totalRecords.ToString());
        }
    }
}
