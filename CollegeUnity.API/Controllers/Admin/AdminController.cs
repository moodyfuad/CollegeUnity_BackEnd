using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Dtos.ResponseDto;
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
        private readonly IAdminServices _adminServices;

        public AdminController(IServiceManager serviceManager)
        {
            _adminServices = serviceManager.AdminServices;
        }

        [HttpGet("Staff")]
        public async Task<IActionResult> Get([Required][FromQuery(Name = "Name")] string name) 
        {
            var response = await _adminServices.SearchStaffBy(name);
            HttpContext.Response.StatusCode = response.StatusCode;
            
            return new JsonResult(response);
            
        }
        [HttpGet("SearchStudents")]
        public async Task<IActionResult> GetStudent([FromQuery] string name) 
        {
            var response = await _adminServices.SearchStudentsBy(name);
            HttpContext.Response.StatusCode = response.StatusCode;

            return new JsonResult(response); 
        }

    }
}
