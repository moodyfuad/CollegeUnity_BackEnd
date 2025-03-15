using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Communites
{
    public interface IManageCommunityFeatures
    {
        // Add Community
        Task<ResultDto> CreateCommunityAsync(CCommunityDto dto);
        // Community Super Admin
        Task<ResultDto> SetSuperAdminForCommunity(int studentId, int communityId);
        // Add Admin
        Task<ResultDto> SetAdminForCommunity(int studentId, int communityId);
        // Edit Info
        Task<ResultDto> EditCommunityInfo(int communityId, UCommunityInfoDto dto);
        // Get Admins
        // Remove Admin
        // Change State
    }
}
