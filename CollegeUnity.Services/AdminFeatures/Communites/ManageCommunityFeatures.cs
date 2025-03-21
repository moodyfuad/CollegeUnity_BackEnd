using CollegeUnity.Contract.AdminFeatures.Communites;
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
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Update;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Delete;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<ResultDto> ChangeCommunityType(int communityId, CommunityType communityType)
        {
            var community = await _repositoryManager.CommunityRepository.GetByIdAsync(communityId);

            if (community == null)
            {
                return new(false, "Community not found.");
            }

            community.CommunityType = communityType;
            await _repositoryManager.CommunityRepository.Update(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> ChangeCommunityState(int communityId, CommunityState communityState)
        {
            var community = await _repositoryManager.CommunityRepository.GetByIdAsync(communityId);

            if (community == null)
            {
                return new(false, "Community not found.");
            }

            community.CommunityState = communityState;
            await _repositoryManager.CommunityRepository.Update(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
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

            var currentCommunity = await _repositoryManager.CommunityRepository.GetByIdAsync(communityId);

            if (currentCommunity == null)
            {
                return new(false, "No community found.");
            }

            if (currentCommunity.CommunityType == CommunityType.LookedPrivate || currentCommunity.CommunityType == CommunityType.LookedPublic)
            {
                return new(false, $"You can't change this status of the community because it's {currentCommunity.CommunityType.ToString()}.");
            }

            // here i do for UCommunityInfoDto mapper and must be updated to be one with CCommunityDto
            var community = dto.ToCommunity<Community>(communityId);

            await _repositoryManager.CommunityRepository.Update(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<PagedList<GCommunityAdminsDto>> GetAdmins(GetStudentCommunityAdminsParameters parameters)
        {
            var admins = await _repositoryManager.StudentCommunityRepository.GetRangeByConditionsAsync(c => c.CommunityId == parameters.communityId, parameters, i => i.Student);
            admins.OrderByDescending(a => a.Role);
            return admins.ToCommunityAdminsMappers();
        }

        public async Task<PagedList<GCommunitesDto>> GetCommunites(GetCommunitesParameters parameters)
        {
            var communites = await _repositoryManager.CommunityRepository.GetRangeAsync(parameters);
            return communites.ToGetCommunites();
        }

        public async Task<PagedList<GCommunitesDto>> GetCommunitesByName(GetCommunitesParameters parameters)
        {
            Expression<Func<Community, bool>> conditions = cm => cm.Name.StartsWith(parameters.Name);
            var communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(conditions, parameters);
            return communites.ToGetCommunites();
        }

        public async Task<PagedList<GCommunitesDto>> GetCommunitesByState(GetCommunitesParameters parameters)
        {
            Expression<Func<Community, bool>> conditions = cm => cm.CommunityState == parameters.CommunityState;

            var communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(conditions, parameters);
            return communites.ToGetCommunites();
        }

        public async Task<PagedList<GCommunitesDto>> GetCommunitesByType(GetCommunitesParameters parameters)
        {
            Expression<Func<Community, bool>> conditions = cm => cm.CommunityType == parameters.CommunityType;

            var communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(conditions, parameters);
            return communites.ToGetCommunites();
        }

        public async Task<ResultDto> RemoveAdminFromCommunites(int studentId, int communityId)
        {
            var studentExist = await _repositoryManager.StudentRepository.GetByIdAsync(studentId);
            var communityExist = await _repositoryManager.CommunityRepository.GetByIdAsync(communityId);

            if (studentExist == null)
            {
                return new(false, "Student not found.");
            }

            if (communityExist == null)
            {
                return new(false, "Community not found.");
            }

            var isAdmin = await _repositoryManager.StudentCommunityRepository
                .AnyAsync(sc => sc.StudentId == studentId && (sc.Role == CommunityMemberRoles.Admin || sc.Role == CommunityMemberRoles.SuperAdmin));

            if (isAdmin)
            {
                return new ResultDto(false, "Student is not Admin in a community.");
            }

            var studentCommunity = RemoveCommunityAdminExtention.ToNormalStudentCommunity(studentId, communityId);
            await _repositoryManager.StudentCommunityRepository.Update(studentCommunity);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> SetAdminForCommunity(int studentId, int communityId)
        {
            if (studentId == null)
            {
                return new(false, "Student not found.");
            }

            if (communityId == null)
            {
                return new(false, "Community not found.");
            }

            var isAdmin = await _repositoryManager.StudentCommunityRepository
                .GetByConditionsAsync(sc => sc.StudentId == studentId && sc.CommunityId == communityId && sc.Role == CommunityMemberRoles.Admin);

            if (isAdmin != null)
            {
                return new ResultDto(true, "Student is already a Admin in a community.");
            }

            var studentCommunity = StudentCommunityExtention.ToAdminStudentCommunity(studentId, communityId);

            await _repositoryManager.StudentCommunityRepository.CreateAsync(studentCommunity);
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
                .GetByConditionsAsync(sc => sc.StudentId == studentId && sc.CommunityId == communityId && sc.Role == CommunityMemberRoles.SuperAdmin);

            if (isSuperAdmin != null)
            {
                return new ResultDto(true, "Student is already a Super Admin in a community.");
            }

            var studentCommunity = StudentCommunityExtention.ToSuperAdminStudentCommunity(studentId, communityId);

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
