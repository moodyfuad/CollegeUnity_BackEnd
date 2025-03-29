using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.StudentFeatures.Courses
{
    public interface IStudentCoursesFeatures
    {
        Task<ApiResponse<bool>> Register(int studentId, int courseId);

        Task<ApiResponse<bool>> UnRegister(int studentId, int courseId);

        Task<ApiResponse<PagedList<GetStudentCoursesResultDto>>> Get(int studentId, GetCoursesForStudentQS queryString);
    }
}
