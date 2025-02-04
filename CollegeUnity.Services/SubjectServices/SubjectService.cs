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

        public async Task<ApiResponse<Subject>> CreateSubjectAsync(CreateSubjectDto dto)
        {
            Subject subject = await _checkSubjectExistsAsync(new CheckSubject(name: dto.Name, major: dto.Major, level: dto.Level, acceptanceType: dto.AcceptanceType));
            return subject == null ? await _createSubjectAsync(dto) : ApiResponse<Subject>.BadRequest(message: "Subject already exists.");
        }

        public async Task<ApiResponse<Subject>> DeleteSubject(int Id)
        {
            Subject subject = await _repositoryManager.SubjectRepository.GetByIdAsync(Id);
            return subject != null ? await _deleteSubject(subject) : ApiResponse<Subject>.NotFound();
        }

        public async Task<ApiResponse<Subject>> UpdateSubject(SubjectDto dto)
        {
            bool isExist = await _repositoryManager.SubjectRepository.IsExistById(dto.Id);
            if (isExist)
            {
                Subject subject = await _checkSubjectExistsAsync(new CheckSubject(name: dto.Name, major: dto.Major, level: dto.Level, acceptanceType: dto.AcceptanceType));
                return subject == null ? await _updateSubject(dto) : ApiResponse<Subject>.BadRequest("Subject already exists with these information.");
            }

            return ApiResponse<Subject>.NotFound();
        }

        public async Task<ApiResponse<IEnumerable<SubjectDto>>> GetAllAsync(SubjectParameters subjectParameters)
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
            return ApiResponse<IEnumerable<SubjectDto>>.Success(subjectDtos);
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
        /// <summary>
        /// a method for create a new subject
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private async Task<ApiResponse<Subject>> _createSubjectAsync(CreateSubjectDto dto)
        {
            Subject subject = dto.MapTo<Subject>();
            //To assign the teacher and creater of the subject
            subject.Teacher = await GetStaffByIdAsync(dto.TeacherId);
            subject.AssignedBy = await GetStaffByIdAsync(dto.HeadOfScientificDepartmentId);
            await _repositoryManager.SubjectRepository.CreateAsync(subject);
            await _repositoryManager.SaveChangesAsync();
            return ApiResponse<Subject>.Created(data: subject);
        }
        #endregion

        #region Private Methods for Update Subject
        public async Task<ApiResponse<Subject>> _updateSubject(SubjectDto dto)
        {
            Subject subject = dto.MapTo<Subject>();
            await _repositoryManager.SubjectRepository.Update(subject);
            await _repositoryManager.SaveChangesAsync();
            return ApiResponse<Subject>.Success(null, "Record updated successfully");
        }
        #endregion

        #region Private Methods for Delete Subject
        public async Task<ApiResponse<Subject>> _deleteSubject(Subject subject)
        {
            await _repositoryManager.SubjectRepository.Delete(subject);
            return ApiResponse<Subject>.Success(null, "Record deleted successfully");
        }
        #endregion
    }
}
