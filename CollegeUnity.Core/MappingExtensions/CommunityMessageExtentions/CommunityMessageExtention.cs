using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.MappingExtensions.CommunityMessageExtentions
{
    public static class CommunityMessageExtention
    {
        public static CommunityMessage ToCommunityMessage(int studentId, int communityId, string content)
        {
            return new()
            {
                CommunityId = communityId,
                Message = content,
                StudentCommunityId = studentId,
                CreatedAt = DateTime.Now,
            };
        }
    }
}
