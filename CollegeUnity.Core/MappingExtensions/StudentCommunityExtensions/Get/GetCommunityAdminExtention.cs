using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.PostExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get
{
    public static class GetCommunityAdminExtention
    {
        private static GCommunityAdminsDto GetCommunityAdmins(this StudentCommunity studentCommunity)
        {
            var admin = new GCommunityAdminsDto 
            {
                Id = studentCommunity.StudentId,
                FullName = $"{studentCommunity.Student.FirstName} {studentCommunity.Student.MiddleName} {studentCommunity.Student.LastName}",
                Role = studentCommunity.Role != null ? (int?)studentCommunity.Role : null
            };
            return admin;
        }

        public static PagedList<GCommunityAdminsDto> ToCommunityAdminsMappers(this PagedList<StudentCommunity> studentCommunities)
        {
            var results = studentCommunities.Select(admin => admin.GetCommunityAdmins()).ToList();

            var pagedList = new PagedList<GCommunityAdminsDto>
            (
                items: results,
                count: studentCommunities.Count(),
                pageNumber: studentCommunities.CurrentPage,
                pageSize: studentCommunities.PageSize
            );
            return pagedList;
        }
    }
}
