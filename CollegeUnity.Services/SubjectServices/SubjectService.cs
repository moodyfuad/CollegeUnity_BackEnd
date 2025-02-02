using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.MappingExtensions.SubjectExtenstions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SubjectServices
{
    public class SubjectService : ISubjectServices
    {
        private readonly IRepositoryManager _repositoryManager;
        public SubjectService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<ApiResponse<Subject>> CreateSubjectAsync(CreateSubjectDto dto)
        {
            Subject subject = await _checkSubjectExistsAsync(dto);
            return subject == null ? await _createSubjectAsync(dto) : ApiResponse<Subject>.BadRequest(message: "Subject already exists.");
        }

        public async Task<ApiResponse<IEnumerable<SubjectDto>>> GetAllAsync()
        {
            IEnumerable<Subject> subjects = await _repositoryManager.SubjectRepository.GetRangeAsync(
                includes: new Expression<Func<Subject, object>>[]
                {
        i => i.Teacher,
        i => i.AssignedBy
                }
            );

            IEnumerable<SubjectDto> subjectDtos = subjects.MapTo<SubjectDto>();
            return ApiResponse<IEnumerable<SubjectDto>>.Success(subjectDtos);
        }

        #region Private Methods
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
        private async Task<Subject> _checkSubjectExistsAsync(CreateSubjectDto dto)
        {
             Subject subject = await _repositoryManager.SubjectRepository.GetByConditionsAsync(
                            s => s.Name == dto.Name &&
                            s.Level == dto.Level &&
                            s.AcceptanceType == dto.AcceptanceType
                        );
            return subject;
        }

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
    }
}
