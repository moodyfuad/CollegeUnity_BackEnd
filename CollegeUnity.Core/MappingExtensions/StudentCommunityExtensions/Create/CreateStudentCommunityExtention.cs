using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create
{
    public static class CreateStudentCommunityExtention
    {
        public static StudentCommunity ToStudentCommunity(int studentId, int communityId)
        {
            StudentCommunity community = new()
            {
                CommunityId = communityId,
                StudentId = studentId,
                Role = Enums.CommunityMemberRoles.SuperAdmin
            };

            return community;
        }
    }
}
