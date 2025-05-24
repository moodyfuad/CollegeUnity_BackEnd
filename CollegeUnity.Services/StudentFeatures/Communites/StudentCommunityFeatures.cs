using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Community;
using CollegeUnity.Core.Dtos.CommunityDtos.Get;
using CollegeUnity.Core.Dtos.CommunityDtos.Update;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.MessagesDto.Get;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Get;
using CollegeUnity.Core.MappingExtensions.CommunityExtensions.Update;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Create;
using CollegeUnity.Core.MappingExtensions.StudentCommunityExtensions.Get;
using LinqKit;
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

        public async Task<PagedList<GCommunityStudentsDto>> GetStudentsOfCommunity(int communityId, CommunityStudentsParameters parameters)
        {
            Expression<Func<StudentCommunity, bool>> conditions = c => c.CommunityId == communityId;

            if (!string.IsNullOrWhiteSpace(parameters.SearchByName))
            {
                var search = parameters.SearchByName.ToLower();
                conditions = conditions.And(n =>
                    (n.Student.FirstName + " " + n.Student.MiddleName + " " + n.Student.LastName)
                        .ToLower()
                        .StartsWith(search)
                );
            }


            Expression<Func<StudentCommunity, object>>[] includes = [i => i.Student];
            var students = await _repositoryManager.StudentCommunityRepository.GetRangeByConditionsAsync(conditions, parameters, includes);
            return students.MapPagedList(GetCommunityStudentsExtention.GetStudents);
        }

        public async Task<PagedList<GCommunityMessagesDto>> GetCommunityMessages(int studentId, int communityId, GetCommunityMessagesParameters parameters)
        {
            await _repositoryManager.StudentCommunityRepository.SetMyLastSeen(studentId, communityId);
            await _repositoryManager.SaveChangesAsync();
            Expression<Func<CommunityMessage, bool>> conditions = s => communityId == s.CommunityId;
            var results = await _repositoryManager.CommunityMessagesRepository.GetRangeByConditionsAsyncDes(conditions, parameters, false, i => i.StudentCommunity, i => i.StudentCommunity.Student);
            return results.MapPagedList(GetCommunityMessageExtention.GetMessage);
        }

        public async Task<ResultDto> UpdateStudentRole(int studentAdminId, UStudentRoleInCommunityDto dto)
        {
            var studentAdminRole = await _repositoryManager.StudentCommunityRepository.GetStudentRoleInCommunity(studentAdminId, dto.CommunityId);
            if (studentAdminRole is not CommunityMemberRoles.SuperAdmin)
            {
                return new(false, "You don't have persmission.");
            }

            var studentRole = await _repositoryManager.StudentCommunityRepository.GetStudentRoleInCommunity(dto.StudentId, dto.CommunityId);
            if (studentRole is CommunityMemberRoles.SuperAdmin)
            {
                return new(false, "You can't change a community super admin role");
            }

            var student = await _repositoryManager.StudentCommunityRepository.GetByConditionsAsync(s => s.StudentId == dto.StudentId && s.CommunityId == dto.CommunityId);

            if (student is not null)
            {
                student.Role = dto.Role;
                await _repositoryManager.StudentCommunityRepository.Update(student);
                await _repositoryManager.SaveChangesAsync();
                return new(true, null);
            }

            return new(false, "Student not found");
        }

        public async Task<PagedList<GStudentCommunitesDto>> GetMyCommunites(int studentId, GetStudentCommunitesParameters parameters)
        {
            var joinedCommunites = await _repositoryManager.StudentCommunityRepository.GetCommunitiesByStudentIdAsync(studentId, false);
            var unreadCounters = new Dictionary<int, int>();

            foreach (var communityId in joinedCommunites)
            {
                int unread = await _repositoryManager.StudentCommunityRepository
                    .GetUnreadMessagesFromLastSeen(studentId, communityId);

                unreadCounters[communityId] = unread;
            }

            PagedList<Community> communites;
            Expression<Func<Community, object>>[] includes = [i => i.CommunityMessages, i => i.CommunityStudents];

            if (string.IsNullOrEmpty(parameters.Name))
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id) && s.CommunityState == Core.Enums.CommunityState.Active, parameters, includes);
            }
            else
            {
                communites = await _repositoryManager.CommunityRepository.GetRangeByConditionsAsync(s => joinedCommunites.Contains(s.Id) && s.Name.StartsWith(parameters.Name) && s.CommunityState == Core.Enums.CommunityState.Active, parameters, includes);
            }

            var result = communites.ToGetStudentCommunites(unreadCounters, studentId);
            return result;
        }

        public async Task<PagedList<GStudentCommunitesDto>> GetNotJoinedCommunities(int studentId, GetStudentCommunitesParameters parameters)
        {
            var joinedCommunities = await _repositoryManager.StudentCommunityRepository.GetCommunitiesByStudentIdAsync(studentId, false);

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

            var isStudentJoined = await _repositoryManager.StudentCommunityRepository.GetByConditionsAsync(s => s.StudentId == studentId && s.CommunityId == communityId);

            if (isStudentJoined != null)
            {
                if (!isStudentJoined.IsDeleted)
                    return new(false, "Student is already joined in the community.");

                isStudentJoined.IsDeleted = false;
                await _repositoryManager.StudentCommunityRepository.Update(isStudentJoined);
            }
            else
            {
                var studentCommunity = StudentCommunityExtention.ToNormalStudentCommunity(studentId, communityId);
                await _repositoryManager.StudentCommunityRepository.CreateAsync(studentCommunity);
            }

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
            else
            {
                if (isStudentJoined.IsDeleted)
                    return new(false, "Student is not in the community.");

                isStudentJoined.Role = CommunityMemberRoles.Normal;
                isStudentJoined.IsDeleted = true;
                await _repositoryManager.StudentCommunityRepository.Update(isStudentJoined);
            }

            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }

        public async Task<ResultDto> EditCommunityInfoByStudent(int studentId, int communityId, UCommunityInfoByStudentDto dto)
        {
            var studentRole = await _repositoryManager.StudentCommunityRepository.GetStudentRoleInCommunity(studentId, communityId);
            if (studentRole is not CommunityMemberRoles.SuperAdmin)
            {
                return new(false, "You don't have persmission.");
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
            var community = dto.ToCommunity<Community>(currentCommunity, communityId);

            _repositoryManager.Detach(currentCommunity);

            await _repositoryManager.CommunityRepository.Update(community);
            await _repositoryManager.SaveChangesAsync();

            return new(true, null);
        }
    }
}
