using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.StudentFeatures.Courses;
using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
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
        public async Task<ApiResponse<PagedList<GetStudentCoursesResultDto>>> Get(int studentId, GetCoursesForStudentQS queryString)
        {
            var courses = !queryString.IsMyCourses ?
                 await this.getAll(queryString) :
                 await this.getRegisteredCourses(studentId, queryString); // Added null-forgiving operator

            var result = GetStudentCoursesResultDto.MapFrom(courses, includeStudents: queryString.IncludeStudents);

            if (result.Count == 0)
            {
                return ApiResponse<PagedList<GetStudentCoursesResultDto>?>.NotFound("No Courses Found");
            }
            else
            {
                return ApiResponse<PagedList<GetStudentCoursesResultDto>>.Success(result, $"[{result.Count}] Courses retrieved")!;
            }
        }

        private async Task<PagedList<Course>> getAll(GetCoursesForStudentQS queryString)
        {
            var courses = await _repositories.CoursesRepository.GetRangeByConditionsAsync(
                    condition: c => c.Name.Contains(queryString.Name) && c.IsDeleted == false,
                    includes : c => c.RegisteredStudents,
                    queryStringParameters: queryString);
            return courses;
        }

        private async Task<PagedList<Course>> getRegisteredCourses(int studentId, GetCoursesForStudentQS queryString)
        {
            var courses = await _repositories.CoursesRepository.GetStudentCourses(studentId, queryString);

            return courses;
        }

        public async Task<ApiResponse<bool>> UnRegister(int studentId, int courseId)
        {
            Course? course = await _repositories.CoursesRepository.GetByConditionsAsync(
                condition: c => c.Id.Equals(courseId),
                includes: c => c.RegisteredStudents);

            var courseQ = await _repositories.CoursesRepository.GetAsQueryable();

            if (course == null)
            {
                return ApiResponse<bool>.NotFound("Course Not Found");
            }
            else if (course.IsDeleted)
            {
                return ApiResponse<bool>.BadRequest("This Course Is Deleted");
            }

            course.RegisteredStudents ??= [];
            if (course.Date.CompareTo(DateTime.Now) <= 0)
            {
                return ApiResponse<bool>.BadRequest("Can not unregistered from ended course");
            }
            else if (course.RegisteredStudents.FirstOrDefault(s => s.Id.Equals(studentId)) == null)
            {
                return ApiResponse<bool>.BadRequest("You Are not Registered For This Course");
            }
            else
            {
                var student = await _repositories.StudentRepository.GetByIdAsync(studentId);
                course.RegisteredStudents.Remove(student);
                await _repositories.SaveChangesAsync();
                return ApiResponse<bool>.Success(true);
            }
        }

        public async Task<ApiResponse<bool>> Register(int studentId, int courseId)
        {
            Course? course = await _repositories.CoursesRepository.GetByConditionsAsync(
                condition: c => c.Id.Equals(courseId),
                includes: c => c.RegisteredStudents);

            if (course == null)
            {
                return ApiResponse<bool>.NotFound("Course Not Found");
            }
            else if (course.IsDeleted)
            {
                return ApiResponse<bool>.BadRequest("This Course Is Deleted");
            }

            course.RegisteredStudents ??= [];
            if (course.AvailableSeats <= course.RegisteredStudents.Count)
            {
                return ApiResponse<bool>.BadRequest("Course Is Full");
            }
            else if (course.RegisteredStudents.FirstOrDefault(s => s.Id.Equals(studentId)) != null)
            {
                return ApiResponse<bool>.BadRequest("You Already Registered For This Course");
            }
            else
            {
                var student = await _repositories.StudentRepository.GetByIdAsync(studentId);
                course.RegisteredStudents.Add(student);
                await _repositories.SaveChangesAsync();
                return ApiResponse<bool>.Success(true);
            }
        }
    }
}
