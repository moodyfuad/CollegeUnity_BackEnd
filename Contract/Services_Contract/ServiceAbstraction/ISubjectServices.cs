using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollegeUnity.Core.Enums;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface ISubjectServices
    {
        Task<bool> CreateSubjectAsync(CreateSubjectDto dto);
        Task<bool> DeleteSubjectAsync(int Id);
        Task<IEnumerable<SubjectDto>?> GetAllAsync(SubjectParameters subjectParameters);
        Task<bool> UpdateSubjectAsync(SubjectDto dto);
        Task<bool> IsExistAsync(int Id);
        Task<bool> SubjectStudyCheck(int subjectId, int teacherId);
        Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType);

    }
}
