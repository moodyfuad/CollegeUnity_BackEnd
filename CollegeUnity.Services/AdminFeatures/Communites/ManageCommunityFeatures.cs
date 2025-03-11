using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Update;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Communites
{
    public class ManageCommunityFeatures : IManageCommunityFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageCommunityFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ResultDto> CreateCommunityAsync(CCommunityDto dto)
        {
            var check = await _checkCommunityFields(dto.Name, dto.Description, dto.CommunityState, dto.CommunityType);

            if (check != null)
            {
                return new(false, check);
            }

            // here i do for CCommunityDto mapper and must be updated to be one with UCommunityInfoDto
            var community = dto.ToCommunity<Community>();

            await _repositoryManager.CommunityRepository.CreateAsync(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> EditCommunityInfo(int communityId, UCommunityInfoDto dto)
        {
            var check = await _checkCommunityFields(dto.Name, dto.Description, dto.CommunityState, dto.CommunityType);

            if (check != null)
            {
                return new(false, check);
            }

            // here i do for UCommunityInfoDto mapper and must be updated to be one with CCommunityDto
            var community = dto.ToCommunity<Community>(communityId);

            await _repositoryManager.CommunityRepository.Update(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> SetSuperAdminForCommunity(int studentId, int communityId)
        {
            if (studentId == null)
            {
                return new(false, "Student not found.");
            }

            if (communityId == null)
            {
                return new(false, "Community not found.");
            }

            var isSuperAdmin = await _repositoryManager.StudentCommunityRepository
                .AnyAsync(sc => sc.StudentId == studentId && sc.Role == CommunityMemberRoles.SuperAdmin);

            if (isSuperAdmin)
            {
                return new ResultDto(true, "Student is already a Super Admin in a community.");
            }

            var studentCommunity = CreateStudentCommunityExtention.ToStudentCommunity(studentId, communityId);

            await _repositoryManager.StudentCommunityRepository.CreateAsync(studentCommunity);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        private async Task<string?> _checkCommunityFields(string? name, string? description, CommunityState? communityState, CommunityType? communityType)
        {
            // Business logic validation
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Community name cannot be empty.";
            }

            var community = await _repositoryManager.CommunityRepository.IsExistByNameAsync(name);

            if (community)
            {
                return "A community with this name already exists.";
            }

            if (description == null)
            {
                return "Description cannot exceed 200 characters.";
            }

            if (description.Length > 200)
            {
                return "Description cannot exceed 200 characters.";
            }

            if (communityState == null)
            {
                return "Community State cannot be empty.";
            }

            if (communityType == null)
            {
                return "Community Type cannot be empty.";
            }

            return null;
        }
    }
}
