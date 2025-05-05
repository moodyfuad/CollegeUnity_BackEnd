using CollegeUnity.Contract.AdminFeatures.Courses;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AdminFeatures.Courses
{
    public class ManageCoursesFeatures : IManageCoursesFeatures
    {
        private readonly IRepositoryManager _repositories;

        public ManageCoursesFeatures(IRepositoryManager repositories)
        {
            _repositories = repositories;
        }

        public async Task<ApiResponse<PagedList<GetStudentCoursesResultDto>?>> Get(GetCoursesForAdminQS queryString)
        {
            var courses = await _repositories.CoursesRepository.GetRangeByConditionsAsync(
                condition: [
                    c => c.Name.Contains(queryString.Name),
                    c => c.Description.Contains(queryString.Description),
                    c => c.Location.Contains(queryString.Location),
                    c => c.LecturerName.Contains(queryString.LecturerName),
                    c => c.IsDeleted == queryString.IncludeDeleted || c.IsDeleted == false
                    ],
                queryStringParameters: queryString,
                includes: c => c.RegisteredStudents,
                trackChanges: false);

            if (courses == null || !courses.Any())
            {
                return ApiResponse<PagedList<GetStudentCoursesResultDto>>.NotFound();
            }

            var result = GetStudentCoursesResultDto.MapFrom(
                courses: courses,
                includeStudents: queryString.IncludeStudents,
                hideDeletedDetails: queryString.HideDeletedDetails);

            return ApiResponse<PagedList<GetStudentCoursesResultDto>>.Success(result)!;
        }

        public async Task<ApiResponse<bool>> Create(CreateCourseDto dto)
        {
            dto.MapTo(out Course result);

            result = await _repositories.CoursesRepository.CreateAsync(result);

            if (result == null)
            {
                return ApiResponse<bool>.BadRequest("Failed Creating the course");
            }

            await _repositories.SaveChangesAsync();
            return ApiResponse<bool>.Created(true);
        }

        public async Task<ApiResponse<bool>> Remove(int courseId, bool ignoreRegisteredStudents = false)
        {
            var course = await _repositories.CoursesRepository.GetByConditionsAsync(
                condition: c => c.Id.Equals(courseId),
                includes: c => c.RegisteredStudents);

            if (course == null)
            {
                return ApiResponse<bool>.BadRequest("Failed Deleting the course", ["Course Not Found To Be Deleted"]);
            }

            if ((course.RegisteredStudents?.Any() ?? false) == true && ignoreRegisteredStudents == false)
            {
                return ApiResponse<bool>.BadRequest(
                    "Failed Deleting The Course",
                    [$"There Are [{course.RegisteredStudents.Count}] Student Registered For This Course"]);
            }

            course.IsDeleted = true;
            await _repositories.CoursesRepository.Update(course);
            await _repositories.SaveChangesAsync();
            return ApiResponse<bool>.Success(true, "Course Deleted");
        }

        public async Task<ApiResponse<bool>> Update(int courseId, CreateCourseDto dto)
        {
            var course = await _repositories.CoursesRepository.GetByIdAsync(courseId);

            if (course == null)
            {
                return ApiResponse<bool>.BadRequest("Failed Updating the course", ["Course Not Found To Be Updated"]);
            }

            _repositories.Detach(course);
            dto.MapTo(out Course result);
            result.Id = course.Id;

            if (await _repositories.CoursesRepository.Update(result) == null)
            {
                return ApiResponse<bool>.InternalServerError("Failed Updating the course");
            }

            await _repositories.SaveChangesAsync();
            return ApiResponse<bool>.Success(true);
        }
    }
}
