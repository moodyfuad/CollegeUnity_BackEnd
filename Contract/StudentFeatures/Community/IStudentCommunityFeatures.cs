using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Community
{
    public interface IStudentCommunityFeatures
    {
        // Get
        Task<PagedList<GStudentCommunitesDto>> GetMyCommunites(int studentId, GetStudentCommunitesParameters parameters);
        Task<PagedList<GStudentCommunitesDto>> GetNotJoinedCommunities(int studentId, GetStudentCommunitesParameters parameters);
        Task<PagedList<GCommunityMessagesDto>> GetCommunityMessages(int studentId, int communityId, GetCommunityMessagesParameters parameters);
        // join
        Task<ResultDto> JoinToCommunity(int studentId, int communityId);
        // Leave
        Task<ResultDto> LeaveFromCommunity(int studentId, int communityId);

        Task<PagedList<GCommunityStudentsDto>> GetStudentsOfCommunity(int communityId, CommunityStudentsParameters parameters);
        Task<ResultDto> EditCommunityInfoByStudent(int studentId, int communityId, UCommunityInfoByStudentDto dto);
        Task<ResultDto> UpdateStudentRole(int studentAdminId, UStudentRoleInCommunityDto dto);
    }
}
