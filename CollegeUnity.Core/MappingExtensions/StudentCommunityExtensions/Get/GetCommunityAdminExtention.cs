using CollegeUnity.Core.Dtos.PostDtos.Get;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Entities;
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
        public static GCommunityAdmins GetCommunityAdmins(this StudentCommunity studentCommunity)
        {
            var admin = new GCommunityAdmins 
            {
                Id = studentCommunity.StudentId,
                FullName = $"{studentCommunity.Student.FirstName} {studentCommunity.Student.MiddleName} {studentCommunity.Student.LastName}",
                Role = studentCommunity.Role != null ? (int?)studentCommunity.Role : null
            };
            return admin;
        }

        public static IEnumerable<GCommunityAdmins> ToCommunityAdminsMappers(this IEnumerable<StudentCommunity> studentCommunities)
        {
            return studentCommunities.Select(admin => admin.GetCommunityAdmins());
        }
    }
}
