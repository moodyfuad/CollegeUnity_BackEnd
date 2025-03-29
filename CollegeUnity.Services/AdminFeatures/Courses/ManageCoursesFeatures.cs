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

        public async Task<ApiResponse<PagedList<Course>>> Get(GetCoursesForAdminQS queryString)
        {
            var courses = await _repositories.CoursesRepository.GetRangeByConditionsAsync(
                condition: [
                    c => c.Name.Contains(queryString.Name),
                    c => c.Description.Contains(queryString.Description),
                    c => c.Location.Contains(queryString.Location),
                    c => c.LecturerName.Contains(queryString.LecturerName),
                    ],
                queryStringParameters: queryString);

            if (courses == null || !courses.Any())
            {
                return ApiResponse<PagedList<Course>>.NotFound();
            }

            return ApiResponse<PagedList<Course>>.Success(courses)!;
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

        public async Task<ApiResponse<bool>> Remove(int courseId)
        {
            var course = await _repositories.CoursesRepository.GetByIdAsync(courseId);

            if (course == null)
            {
                return ApiResponse<bool>.BadRequest("Failed Deleting the course", ["Course Not Found To Be Deleted"]);
            }


            #error the course can not be deleted due to the associated regiestered students //

            if (await _repositories.CoursesRepository.Delete(course) == null)
            {
                return ApiResponse<bool>.InternalServerError("Failed Deleting the course");
            }

            await _repositories.SaveChangesAsync();

            return ApiResponse<bool>.Success(true);
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
