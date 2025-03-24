using CollegeUnity.Core.Dtos.FailureResualtDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Subject
{
    public interface IManageSubjectFeatures
    {
        Task<ResultDto> CreateSubjectAsync(CSubjectDto dto);
        Task<ResultDto> DeleteSubjectAsync(int Id);
        Task<ResultDto> UpdateSubjectAsync(int subjectId, USubjectDto dto);
        Task<ResultDto> AssignSubjectToTeacher(int teacherId, int subjectId);
        //Task<bool> IsExistAsync(int Id);
        Task<bool> SubjectStudyCheck(int subjectId, int teacherId);
        Task<PagedList<GSubjectDto>> GetSubjects(GetSubjectParameters parameters);
    }
}
