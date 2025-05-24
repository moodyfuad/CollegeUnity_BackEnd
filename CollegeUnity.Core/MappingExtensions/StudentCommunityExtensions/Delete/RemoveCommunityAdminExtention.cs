using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Delete
{
    public static class RemoveCommunityAdminExtention
    {
        public static StudentCommunity ToNormalStudentCommunity(int id, int studentId, int communityId)
        {
            StudentCommunity community = new()
            {
                Id = id,
                CommunityId = communityId,
                StudentId = studentId,
                Role = Enums.CommunityMemberRoles.Normal
            };

            return community;
        }

        public static StudentCommunity ToNormalStudentCommunity(int studentId, int communityId)
        {
            StudentCommunity community = new()
            {
                CommunityId = communityId,
                StudentId = studentId,
                Role = Enums.CommunityMemberRoles.Normal
            };

            return community;
        }

    }
}
