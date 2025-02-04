using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Helpers
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public bool HasPrevious => CurrentPage < 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalCount = count;
            PageSize = pageSize;

            TotalPages = (int)Math.Ceiling((count / (double)PageSize));
            
            AddRange(items);
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int pageNumber = 1, int pageSize = 20)
        {
            var count = source.Count();
            var items = await source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize); 
        }
    }
}
