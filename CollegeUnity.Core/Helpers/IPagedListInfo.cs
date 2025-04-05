using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Helpers
{
    public interface IPagedListInfo : IList
    {
        int CurrentPage { get; }

        int TotalPages { get; }

        int PageSize { get; }

        int TotalCount { get; }

        bool HasPrevious { get; }

        bool HasNext { get; }
    }
}
