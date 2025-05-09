using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StaffFeatures.Students
{
    public interface IGetMyStudents
    {
        Task<PagedList<GStudentDto>> MyStudents(int staffId, GetMyStudentsParameters parameters);
    }
}
