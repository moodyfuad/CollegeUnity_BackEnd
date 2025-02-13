using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.MappingExtensions.SubjectExtenstions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SubjectServices
{
    public class SubjectService : ISubjectServices
    {
        #region Fields
        private readonly IRepositoryManager _repositoryManager;
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

        #endregion
        public SubjectService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<bool> IsExistAsync(int Id)
        {
            return await _repositoryManager.SubjectRepository.IsExistById(Id);
        }

        public async Task<bool> CreateSubjectAsync(CreateSubjectDto dto)
        {
            Subject subject = await _checkSubjectExistsAsync(new CheckSubject(name: dto.Name, major: dto.Major, level: dto.Level, acceptanceType: dto.AcceptanceType));
            
            if (subject == null)
            {
                await _createSubjectAsync(dto);
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSubjectAsync(int Id)
        {
            return await _deleteSubject(Id);
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

        public async Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType)
        {
            return await _repositoryManager.SubjectRepository.GetDistinctSubjects(level, major, acceptanceType);
        }

        public async Task<IEnumerable<SubjectDto>> GetAllAsync(SubjectParameters subjectParameters)
        {
            IEnumerable<Subject> subjects = await _repositoryManager.SubjectRepository.GetRangeAsync(
                subjectParameters,
                includes: new Expression<Func<Subject, object>>[]
                {
                    i => i.Teacher,
                    i => i.AssignedBy
                }
            );

            IEnumerable<SubjectDto> subjectDtos = subjects.MapTo<SubjectDto>();
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
                           s.Major == checkSubject.Major  &&
                           s.Level == checkSubject.Level &&
                           s.AcceptanceType == checkSubject.AcceptanceType
                       );
            return subject;
        }
        #endregion

        #region Private Methods for Create Subject
        private async Task<Subject> _createSubjectAsync(CreateSubjectDto dto)
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

        #region Private Methods for Delete Subject
        public async Task<bool> _deleteSubject(int Id)
        {
            await _repositoryManager.SubjectRepository.Delete(Id);
            await _repositoryManager.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
