using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Subject
{
    public interface IManageSubjectFeatures
    {
        Task<bool> CreateSubjectAsync(CSubjectDto dto);
        Task<bool> DeleteSubjectAsync(int Id);
        Task<IEnumerable<GSubjectDto>?> GetAllAsync(SubjectParameters subjectParameters);
        Task<bool> UpdateSubjectAsync(SubjectDto dto);
        //Task<bool> IsExistAsync(int Id);
        Task<bool> SubjectStudyCheck(int subjectId, int teacherId);
        Task<IEnumerable<GSubjectDto>?> GetSubjectsByName(GetSubjectByNameParameters parameters);
    }
}
