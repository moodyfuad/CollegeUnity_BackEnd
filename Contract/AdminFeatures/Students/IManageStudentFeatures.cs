using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Student
{
    public interface IManageStudentFeatures
    {
        Task<PagedList<GStudentDto>> GetStudents(GetStudentParameters parameters);
        Task<PagedList<GStudentDto>> GetStudentSignUpRequest(GetStudentSignUpParameters parameters);
    }
}
