using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PagedList<GStudentCommunitesDto>> GetMyCommunites(int studentId, GetStudentCommunitesParameters parameters)
        {
            var joinedCommunites = await _repositoryManager.StudentCommunityRepository.GetCommunitiesByStudentIdAsync(studentId);
            PagedList<Community> communites;

            if (string.IsNullOrEmpty(parameters.Name))
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id), parameters);
            }
            else
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id) && s.Name.StartsWith(parameters.Name), parameters);
            }

            var result = communites.ToGetStudentCommunites();
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

            var isStudentJoined = _repositoryManager.StudentCommunityRepository.GetByConditionsAsync(s => s.StudentId == studentId && s.CommunityId == communityId);

            if (isStudentJoined != null)
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
