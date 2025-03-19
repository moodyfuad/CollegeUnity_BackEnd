using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
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
    }
}
