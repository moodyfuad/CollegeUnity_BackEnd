using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Subjects
{
    public interface IGetMySubjects
    {
        Task<List<DistinctSubjectDto>> MySubjects(int teacherId);
    }
}
