using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Community
{
    public interface ICommunityFeatures
    {
        IStudentCommunityFeatures StudentCommunity { get; }

        IManageCommunityFeatures ManageCommunity { get; }

        IManageCommunityMembersFeatures CommunityMembers { get; }

        IManageCommunityMessagesFeatures CommunityMessages { get; }
    }
}
