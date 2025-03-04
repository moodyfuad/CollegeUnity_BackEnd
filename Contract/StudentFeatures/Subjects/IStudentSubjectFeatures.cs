using CollegeUnity.Core.Dtos.InterestedSubjectDtos;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Subjects
{
    public interface IStudentSubjectFeatures
    {
        Task<List<int>> GetStudentSubject(Level level, Major major, AcceptanceType acceptanceType);
        Task<IEnumerable<GInterestedSubjectDto>?> GetStudentIntrestedSubject(GetInterestedSubjectParameters parameters, int studentId);
    }
}
