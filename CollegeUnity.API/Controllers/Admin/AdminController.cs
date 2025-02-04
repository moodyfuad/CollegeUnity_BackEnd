using CollegeUnity.API.Controllers.Student;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.QueryStrings;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace CollegeUnity.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AdminController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("Staff")]
        public async Task<IActionResult> Get([Required][FromQuery(Name = "Name")] string name, StaffParameters staffParameters) 
        {
            var response = await _serviceManager.AdminServices.SearchStaffBy(name,staffParameters);
            HttpContext.Response.StatusCode = response.StatusCode;
            
            return new JsonResult(response); 
        }
        [HttpGet("SearchStudents")]
        public async Task<IActionResult> GetStudent([FromQuery] StudentSearchParameters parameters) 
        {
            //return await new StudentController(serviceManager: _serviceManager);
           /* var response = await _serviceManager.StudentServices.GetStudentsAsync(parameters);
            HttpContext.Response.StatusCode = response.StatusCode;*/

            return Ok("Not implemented"); 
        }

        [HttpPost("CreateSubject")]
        public async Task<IActionResult> CreateNewSubject([FromForm]CreateSubjectDto dto)
        {
            var response = await _serviceManager.SubjectServices.CreateSubjectAsync(dto);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response);
        }

        [HttpGet("GetSubjects")]
        public async Task<IActionResult> GetSubjects(SubjectParameters subjectParameters)
        {
            var response = await _serviceManager.SubjectServices.GetAllAsync(subjectParameters);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response);
        }

        [HttpDelete("DeleteSubject/{Id}")]
        public async Task<IActionResult> DeleteSubject(int Id, StaffParameters staffParameters)
        {
            var response = await _serviceManager.SubjectServices.DeleteSubject(Id);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response);
        }

        [HttpPut("UpdateSubject")]
        public async Task<IActionResult> UpdateSubject([FromForm] SubjectDto dto)
        {
            var response = await _serviceManager.SubjectServices.UpdateSubject(dto);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response);
        }

    }
}
