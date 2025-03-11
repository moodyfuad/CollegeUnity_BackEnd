using CollegeUnity.Contract.AdminFeatures.Communites;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.CommunityDtos.Create;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Create;
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
            var check = await _checkCommunityFields(dto);

            if (check != null)
            {
                return new(false, check);
            }

            // Map DTO to entity and save
            var community = dto.ToCommunity<Community>();

            await _repositoryManager.CommunityRepository.CreateAsync(community);
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

        private async Task<string?> _checkCommunityFields(CCommunityDto dto)
        {
            // Business logic validation
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return "Community name cannot be empty.";
            }

            var community = await _repositoryManager.CommunityRepository.IsExistByNameAsync(dto.Name);

            if (community)
            {
                return "A community with this name already exists.";
            }

            if (dto.Description.Length > 200)
            {
                return "Description cannot exceed 200 characters.";
            }

            if (dto.CommunityState == null)
            {
                return "Community State cannot be empty.";
            }

            if (dto.CommunityType == null)
            {
                return "Community Type cannot be empty.";
            }

            return null;
        }
    }
}
