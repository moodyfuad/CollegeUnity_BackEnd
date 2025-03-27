using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using CollegeUnity.Core.MappingExtensions.SubjectExtenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Subjects
{
    public class ManageSubjectFeatures : IManageSubjectFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageSubjectFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private async Task<bool> isExistByIdAsync(int Id)
        {
            return await _repositoryManager.SubjectRepository.IsExistById(Id);
        }

        private async Task<bool> hasRoleTeacher(int? teacherId)
        {
            return await _repositoryManager.StaffRepository.GetByConditions(s => s.Id == teacherId && s.Roles.Contains(Roles.Teacher));
        }

        public async Task<ResultDto> CreateSubjectAsync(CSubjectDto dto)
        {
            var check = await _checkCreateNewSubjectAsync(dto);

            if (check == null)
            {
                var subject = dto.MapTo();
                var response = await ProcessSubjectAsync(subject, subject.TeacherId, true);
                return response;
            }

            return new(true, "Subject already exists.");
        }

        public async Task<ResultDto> UpdateSubjectAsync(int subjectId, USubjectDto dto)
        {
            var check = await _checkUpdateSubjectAsync(subjectId, dto);

            if (check == null)
            {
                Subject subject = await _repositoryManager.SubjectRepository.GetByIdAsync(subjectId);
                var newSubject = dto.MapTo(subject);
                _repositoryManager.Detach(subject);
                return await ProcessSubjectAsync(newSubject, dto.TeacherId.Value, false);
            }

            return new(false, "Subject found with these information.");
        }

        public async Task<ResultDto> AssignSubjectToTeacher(int teacherId, int subjectId)
        {
            var subject = await _repositoryManager.SubjectRepository.GetByIdAsync(subjectId);

            if (subject == null)
            {
                return new(false, "Subject not found.");
            }

            var staff = await _repositoryManager.StaffRepository.GetByIdAsync(teacherId);

            if (staff == null)
            {
                return new(false, "Teacher not found.");
            }

            if (await SubjectStudyCheck(subjectId, teacherId))
            {
                return new(false, "Subject is already assigned to this teacher.");
            }

            if (!await hasRoleTeacher(staff.Id))
            {
                return new(false, "Only teacher assigned.");
            }

            subject.TeacherId = teacherId;
            await _repositoryManager.SubjectRepository.Update(subject);
            await _repositoryManager.SaveChangesAsync();
            return new(true, "Subject assigned successfully.");
        }


        public async Task<ResultDto> DeleteSubjectAsync(int Id)
        {
            if (await isExistByIdAsync(Id))
            {
                await _repositoryManager.SubjectRepository.Delete(Id);
                await _repositoryManager.SaveChangesAsync();
                return new(true, null);
            }
            return new(false, "Subject not found.");
        }
        public async Task<PagedList<GSubjectDto>?> GetSubjects(GetSubjectParameters parameters)
        {
            PagedList<Subject> subjects = await _repositoryManager.SubjectRepository.GetRangeByConditionsAsync(
                s => (string.IsNullOrEmpty(parameters.Name) || s.Name.Contains(parameters.Name)) &&
                     (!parameters.Major.HasValue || s.Major == parameters.Major) &&
                     (!parameters.Level.HasValue || s.Level == parameters.Level) &&
                     (!parameters.AcceptanceType.HasValue || s.AcceptanceType == parameters.AcceptanceType),
                parameters,
                includes:
                [
                    i => i.Teacher,
                    i => i.AssignedBy
                ]
            );

            return subjects.MapTo();
        }

        public async Task<bool> SubjectStudyCheck(int subjectId, int teacherId)
        {
            Subject subject = await _repositoryManager.SubjectRepository.GetByConditionsAsync(
                condition: s => s.TeacherId == teacherId && s.Id == subjectId
            );

            if (subject != null)
                return true;
            else
                return false;
        }

        #region Private Methods
        /// <summary>
        /// method to check if the subject is already Subject exists
        /// </summary>
        /// <param Name="dto"></param>
        /// <returns></returns>
        private async Task<Subject> _checkCreateNewSubjectAsync(CSubjectDto dto)
        {
            Subject subject = await _repositoryManager.SubjectRepository.GetByConditionsAsync(
                           s => s.Name == dto.Name &&
                           s.Major == dto.Major &&
                           s.Level == dto.Level &&
                           s.AcceptanceType == dto.AcceptanceType
                       );
            return subject;
        }

        private async Task<Subject> _checkUpdateSubjectAsync(int subjectId, USubjectDto dto)
        {
            Subject subject = await _repositoryManager.SubjectRepository.GetByConditionsAsync(
                           s => s.Name == dto.Name &&
                           s.Major == dto.Major &&
                           s.Level == dto.Level &&
                           s.AcceptanceType == dto.AcceptanceType &&
                           s.Id != subjectId
                       );
            return subject;
        }

        private async Task<ResultDto> ProcessSubjectAsync(Subject subject, int? teacherId, bool isCreateOperation)
        {
            if (teacherId != null)
            {
                if (await hasRoleTeacher((int)teacherId))
                {
                    subject.TeacherId = teacherId;
                }
                else
                {
                    return new(false, "Only teacher assigned.");
                }
            }

            if (isCreateOperation)
            {
                await _repositoryManager.SubjectRepository.CreateAsync(subject);
            }
            else
            {
                await _repositoryManager.SubjectRepository.Update(subject);
            }

            await _repositoryManager.SaveChangesAsync();
            return new(true, null);
        }
        #endregion
    }
}
