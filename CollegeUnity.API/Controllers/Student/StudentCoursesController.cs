using CollegeUnity.API.Middlerware_Extentions;
using CollegeUnity.Contract.StudentFeatures.Courses;
using CollegeUnity.Core.Dtos.CourseDtos;
using CollegeUnity.Core.Enums;
using CollegeUnity.Services.StudentFeatures.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Student
{
    [Route("api/Student/Courses")]
    [ApiController]
    [Authorize(Roles = nameof(Roles.Student))]
    public class StudentCoursesController : ControllerBase
    {
        private readonly IStudentCoursesFeatures _studentCoursesFeatures;

        public StudentCoursesController(IStudentCoursesFeatures studentCoursesFeatures)
        {
            _studentCoursesFeatures = studentCoursesFeatures;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetStudentCoursesQS queryString)
        {
            int studentId = User.GetUserId();

            var result = await _studentCoursesFeatures.Get(studentId, queryString);

            return new JsonResult(result);
        }
    }
}
