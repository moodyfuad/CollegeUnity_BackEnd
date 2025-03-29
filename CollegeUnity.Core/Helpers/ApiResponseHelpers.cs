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

        public static void AddPagination<T>(this HttpResponse response, PagedList<T>? pl)
        {
            var meta = new
            {
                PageNumber = pl?.CurrentPage??0,
                TotalPages = pl?.TotalPages??0,
                PageSize = pl?.PageSize ?? 0,
                TotalCount = pl?.TotalCount ?? 0,
                HasPrevious = pl?.HasPrevious?? false,
                HasNext = pl?.HasNext?? false,
            };
            response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
        }
    }
}
