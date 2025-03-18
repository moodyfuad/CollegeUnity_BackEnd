using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Helpers
{
    public static class ApiResponseHelpers
    {
        public static void AddPagination(
            this HttpResponse response,
            int pageNumber,
            int totalPages,
            int pageSize,
            bool hasPrevious,
            bool hasNext)
        {
            var meta = new
            {
                PageNumber = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize,
                HasPrevious = hasPrevious,
                HasNext = hasNext,
            };

            response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
        }

        public static void AddPagination<T>(this HttpResponse response, PagedList<T> pl)
        {
            var meta = new
            {
                PageNumber = pl.CurrentPage,
                TotalPages = pl.TotalPages,
                PageSize = pl.PageSize,
                TotalCount = pl.TotalCount,
                HasPrevious = pl.HasPrevious,
                HasNext = pl.HasNext,
            };
            response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
        }
    }
}
