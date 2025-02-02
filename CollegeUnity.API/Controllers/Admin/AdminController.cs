using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SubjectDtos;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get([Required][FromQuery(Name = "Name")] string name) 
        {
            var response = await _serviceManager.AdminServices.SearchStaffBy(name);
            HttpContext.Response.StatusCode = response.StatusCode;
            
            return new JsonResult(response);
            
        }
        [HttpGet("SearchStudents")]
        public async Task<IActionResult> GetStudent([FromQuery] string name) 
        {
            var response = await _serviceManager.AdminServices.SearchStudentsBy(name);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response); 
        }

        [HttpPost("CreateSubject")]
        public async Task<IActionResult> CreateNewSubject([FromForm]CreateSubjectDto dto)
        {
            var response = await _serviceManager.SubjectServices.CreateSubjectAsync(dto);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response);
        }

    }
}
