using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get
{
    public static class GetStudentCommunitesExtention
    {
        private static GStudentCommunitesDto GetCommunity(this Community community)
        {
            return new GStudentCommunitesDto
            {
                Id = community.Id,
                Name = community.Name,
                Description = community.Description,
            };
        }

        public static PagedList<GStudentCommunitesDto> ToGetStudentCommunites(this PagedList<Community> communities)
        {
            var results = communities.Select(c => c.GetCommunity()).ToList();
            var pagedList = new PagedList<GStudentCommunitesDto>
                (
                    items: results,
                    count: communities.Count(),
                    pageNumber: communities.CurrentPage,
                    pageSize: communities.PageSize
                );
            return pagedList;
        }
    }
}
