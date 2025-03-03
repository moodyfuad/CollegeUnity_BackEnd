using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StaffFeatures.Subject;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.SubjectExtenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StaffFeatures.Subjects
{
    public class ManageSubjectFeatures : IManageSubjectFeatures
    {
        private readonly IRepositoryManager _repositoryManager;

        public ManageSubjectFeatures(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        private class CheckSubject
        {
            public string Name { get; internal set; }
            public Major Major { get; set; }
            public Level Level { get; set; }
            public AcceptanceType AcceptanceType { get; set; }

            public CheckSubject(string name, Major major, Level level, AcceptanceType acceptanceType)
            {
                Name = name;
                Major = major;
                Level = level;
                AcceptanceType = acceptanceType;
            }
        }

        public async Task<bool> CreateSubjectAsync(CSubjectDto dto)
        {
            var subject = await _checkSubjectExistsAsync(new CheckSubject(name: dto.Name, major: dto.Major, level: dto.Level, acceptanceType: dto.AcceptanceType));

            if (subject == null)
            {
                await _createSubjectAsync(dto);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSubjectAsync(int Id)
        {
            await _repositoryManager.SubjectRepository.Delete(Id);
            await _repositoryManager.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSubjectAsync(SubjectDto dto)
        {
            Subject subject = await _checkSubjectExistsAsync(new CheckSubject(name: dto.Name, major: dto.Major, level: dto.Level, acceptanceType: dto.AcceptanceType));

            if (subject == null)
            {
                await _updateSubject(dto);
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<GSubjectDto>?> GetSubjectsByName(GetSubjectByNameParameters parameters)
        {
            IEnumerable<Subject> subjects = await _repositoryManager.SubjectRepository.GetRangeByConditionsAsync(
                s => s.Name.Contains(parameters.SubjectName),
                parameters,
                includes:
                [
                    i => i.Teacher,
                    i => i.AssignedBy
                ]
            );

            IEnumerable<GSubjectDto> subjectDtos = subjects.MapTo<GSubjectDto>();
            return subjectDtos;
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

        public async Task<IEnumerable<GSubjectDto>> GetAllAsync(SubjectParameters subjectParameters)
        {
            IEnumerable<Subject> subjects = await _repositoryManager.SubjectRepository.GetRangeAsync(
                subjectParameters,
                includes:
                [
                    i => i.Teacher,
                    i => i.AssignedBy
                ]
            );

            IEnumerable<GSubjectDto> subjectDtos = subjects.MapTo<GSubjectDto>();
            return subjectDtos;
        }

        #region Shared Private Methods
        /// <summary>
        /// method to check if the staff id is exists
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        private async Task<Staff> GetStaffByIdAsync(int? staffId)
        {
            if (staffId.HasValue)
            {
                return await _repositoryManager.StaffRepository.GetByIdAsync(staffId.Value);
            }
            return null;
        }

        /// <summary>
        /// method to check if the subject is already Subject exists
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<Subject> _checkSubjectExistsAsync(CheckSubject checkSubject)
        {
            Subject subject = await _repositoryManager.SubjectRepository.GetByConditionsAsync(
                           s => s.Name == checkSubject.Name &&
                           s.Major == checkSubject.Major &&
                           s.Level == checkSubject.Level &&
                           s.AcceptanceType == checkSubject.AcceptanceType
                       );
            return subject;
        }
        #endregion

        #region Private Methods for Create Subject
        private async Task<Subject> _createSubjectAsync(CSubjectDto dto)
        {
            Subject subject = dto.MapTo<Subject>();
            //To assign the teacher and creater of the subject
            subject.Teacher = await GetStaffByIdAsync(dto.TeacherId);
            subject.AssignedBy = await GetStaffByIdAsync(dto.HeadOfScientificDepartmentId);
            await _repositoryManager.SubjectRepository.CreateAsync(subject);
            await _repositoryManager.SaveChangesAsync();
            return subject;
        }
        #endregion
        #region Private Methods for Update Subject
        public async Task<Subject> _updateSubject(SubjectDto dto)
        {
            Subject subject = dto.MapTo<Subject>();
            await _repositoryManager.SubjectRepository.Update(subject);
            await _repositoryManager.SaveChangesAsync();
            return subject;
        }
        #endregion
    }
}
