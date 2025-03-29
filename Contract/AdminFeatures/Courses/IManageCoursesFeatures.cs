using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.AdminFeatures.Courses
{
    public interface IManageCoursesFeatures
    {
        Task<ApiResponse<PagedList<Course>>> Get(GetCoursesForAdminQS queryString);

        Task<ApiResponse<bool>> Create(CreateCourseDto dto);

        Task<ApiResponse<bool>> Remove(int courseId, bool ignoreRegisteredStudents = false);

        Task<ApiResponse<bool>> Update(int courseId, CreateCourseDto dto);
    }
}
