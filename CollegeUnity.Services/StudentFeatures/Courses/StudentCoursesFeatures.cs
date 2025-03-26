using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Courses;
using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.StudentFeatures.Courses
{
    public class StudentCoursesFeatures(IRepositoryManager _repositories) : IStudentCoursesFeatures
    {
        public async Task<ApiResponse<PagedList<GetStudentCoursesResultDto>>> Get(int studentId, GetStudentCoursesQS queryString)
        {
            var courses = await _repositories.CoursesRepository.GetRangeByConditionsAsync(
                condition: c => c.Name.Contains(queryString.Name),
                queryStringParameters: queryString);

            var result = GetStudentCoursesResultDto.MapFrom(courses);

            if (result.Count == 0)
            {
                return ApiResponse<PagedList<GetStudentCoursesResultDto>>.NotFound("No Courses Found");
            }
            else
            {
                return ApiResponse<PagedList<GetStudentCoursesResultDto>>.Success(result, $"[{result.Count}] Courses retrieved")!;
            }
        }

        public Task<ApiResponse<bool>> Register(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> UnRegister(int studentId, int courseId)
        {
            throw new NotImplementedException();
        }
    }
}
