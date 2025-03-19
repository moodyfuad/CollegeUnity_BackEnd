using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentCommunityDtos.Get;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
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
        Task<PagedList<GCommunityAdminsDto>> GetAdmins(GetStudentCommunityAdminsParameters parameters);
        //Get Communites
        Task<PagedList<GCommunitesDto>> GetCommunites(GetCommunitesParameters parameters);
        Task<PagedList<GCommunitesDto>> GetCommunitesByName(GetCommunitesParameters parameters);
        Task<PagedList<GCommunitesDto>> GetCommunitesByState(GetCommunitesParameters parameters);
        Task<PagedList<GCommunitesDto>> GetCommunitesByType(GetCommunitesParameters parameters);
        // Remove Admin
        Task<ResultDto> RemoveAdminFromCommunites(int studentId, int communityId);
        // Change State
        Task<ResultDto> ChangeCommunityType(int communityId, CommunityType communityType);
        Task<ResultDto> ChangeCommunityState(int communityId, CommunityState communityState);
    }
}
