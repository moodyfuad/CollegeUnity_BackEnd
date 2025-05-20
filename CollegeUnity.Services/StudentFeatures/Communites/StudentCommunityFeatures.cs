using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Communites
{
    public class StudentCommunityFeatures : IStudentCommunityFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public StudentCommunityFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<PagedList<GCommunityMessagesDto>> GetCommunityMessages(int studentId, int communityId, GetCommunityMessagesParameters parameters)
        {
            await _repositoryManager.StudentCommunityRepository.SetMyLastSeen(studentId, communityId);
            await _repositoryManager.SaveChangesAsync();
            Expression<Func<CommunityMessage, bool>> conditions = s => communityId == s.CommunityId;
            var results = await _repositoryManager.CommunityMessagesRepository.GetRangeByConditionsAsync(conditions, parameters, i => i.StudentCommunity, i => i.StudentCommunity.Student);
            return results.MapPagedList(GetCommunityMessageExtention.GetMessage);
        }

        public async Task<PagedList<GStudentCommunitesDto>> GetMyCommunites(int studentId, GetStudentCommunitesParameters parameters)
        {
            var joinedCommunites = await _repositoryManager.StudentCommunityRepository.GetCommunitiesByStudentIdAsync(studentId);
            var unreadCounters = new Dictionary<int, int>();

            foreach (var communityId in joinedCommunites)
            {
                int unread = await _repositoryManager.StudentCommunityRepository
                    .GetUnreadMessagesFromLastSeen(studentId, communityId);

                unreadCounters[communityId] = unread;
            }

            PagedList<Community> communites;
            Expression<Func<Community, object>>[] includes = [i => i.CommunityMessages];

            if (string.IsNullOrEmpty(parameters.Name))
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id) && s.CommunityState == Core.Enums.CommunityState.Active, parameters, includes);
            }
            else
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id) && s.Name.StartsWith(parameters.Name) && s.CommunityState == Core.Enums.CommunityState.Active, parameters, includes);
            }

            var result = communites.ToGetStudentCommunites(unreadCounters);
            return result;
        }

        public async Task<PagedList<GStudentCommunitesDto>> GetNotJoinedCommunities(int studentId, GetStudentCommunitesParameters parameters)
        {
            var joinedCommunities = await _repositoryManager.StudentCommunityRepository.GetCommunitiesByStudentIdAsync(studentId);

            PagedList<Community> communities;

            if (string.IsNullOrEmpty(parameters.Name))
            {
                communities = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(
                    s => !joinedCommunities.Contains(s.Id) && s.CommunityState == Core.Enums.CommunityState.Active,
                    parameters);
            }
            else
            {
                communities = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(
                    s => !joinedCommunities.Contains(s.Id) && s.Name.StartsWith(parameters.Name) && s.CommunityState == Core.Enums.CommunityState.Active,
                    parameters);
            }

            var result = communities.ToGetStudentCommunites();
            return result;
        }

        public async Task<ResultDto> JoinToCommunity(int studentId, int communityId)
        {
            var student = await _repositoryManager.StudentRepository.ExistsAsync(studentId);
            if (!student)
            {
                return new(false, "Studnet not found.");
            }

            var community = await _repositoryManager.CommunityRepository.ExistsAsync(communityId);
            if (!community)
            {
                return new(false, "Community not found.");
            }

            var isStudentJoined = await _repositoryManager.StudentCommunityRepository.AnyAsync(s => s.StudentId == studentId && s.CommunityId == communityId);

            if (isStudentJoined)
            {
                return new(false, "Student is already joined in the community.");
            }

            var studentCommunity = StudentCommunityExtention.ToNormalStudentCommunity(studentId, communityId);
            await _repositoryManager.StudentCommunityRepository.CreateAsync(studentCommunity);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> LeaveFromCommunity(int studentId, int communityId)
        {
            var student = await _repositoryManager.StudentRepository.ExistsAsync(studentId);
            if (!student)
            {
                return new(false, "Student not found.");
            }

            var community = await _repositoryManager.CommunityRepository.ExistsAsync(communityId);
            if (!community)
            {
                return new(false, "Community not found.");
            }

            var isStudentJoined = await _repositoryManager.StudentCommunityRepository
                .GetByConditionsAsync(s => s.StudentId == studentId && s.CommunityId == communityId);

            if (isStudentJoined == null)
            {
                return new(false, "Student is not a member of the community.");
            }

            await _repositoryManager.StudentCommunityRepository.Delete(isStudentJoined);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
