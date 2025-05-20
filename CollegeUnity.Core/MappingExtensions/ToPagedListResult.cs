using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions
{
    public static class ToPagedListResult
    {
        public static PagedList<TDestination> MapPagedList<TSource, TDestination>(
                this PagedList<TSource> source,
                Func<TSource, TDestination> converter)
        {
            var results = source.Select(converter).ToList();

            return new PagedList<TDestination>(
                items: results,
                count: source.TotalCount,
                pageNumber: source.CurrentPage,
                pageSize: source.PageSize
            );
        }
    }
}
