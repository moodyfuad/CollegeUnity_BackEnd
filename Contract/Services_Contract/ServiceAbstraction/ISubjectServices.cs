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
        //Done
        Task<bool> CreateSubjectAsync(CSubjectDto dto);
        //Done
        Task<bool> DeleteSubjectAsync(int Id);
        //Done
        Task<IEnumerable<GSubjectDto>?> GetAllAsync(SubjectParameters subjectParameters);
        //Done
        Task<bool> UpdateSubjectAsync(SubjectDto dto);
        //Done
        Task<bool> IsExistAsync(int Id);
        Task<bool> SubjectStudyCheck(int subjectId, int teacherId);
        Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType);
        //Done
        Task<IEnumerable<GSubjectDto>?> GetSubjectsByName(GetSubjectByNameParameters parameters);

    }
}
