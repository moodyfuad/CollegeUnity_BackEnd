using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
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
        Task<IEnumerable<GCommunityAdminsDto>> GetAdmins(GetStudentCommunityAdminsParameters parameters);
        //Get Communites
        Task<IEnumerable<GCommunitesDto>> GetCommunites(GetCommunitesParameters parameters);
        // Remove Admin
        // Change State
    }
}
