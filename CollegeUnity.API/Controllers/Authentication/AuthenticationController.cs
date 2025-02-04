using CollegeUnity.Core.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CollegeUnity.Core.Enums;
using CollegeUnity.Services.AuthenticationServices;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Contract;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Dtos.ResponseDto;

namespace CollegeUnity.API.Controllers.Authentication
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IServiceManager _serviceManager;

        public AuthenticationController(IConfiguration configuration, IServiceManager serviceManager)
        {
            _config = configuration;
            _serviceManager = serviceManager;
        }

        [HttpPost("student/login")]
        public async Task<IActionResult> StudentLogin([FromForm] StudentLoginDto student)
        {
            var result = await _serviceManager.AuthenticationService.Login(student);
            if (result.Success)
            {
                var response = ApiResponse<StudentLoginDto>.Success(data: (StudentLoginDto)result);
                return new JsonResult(response);
            }
            else
            {
                string errors = "";
                foreach (var msg in result.ErrorMessages) errors += msg;
                var response = ApiResponse<StudentLoginDto>.NotFound(errors);
                return new JsonResult(response);
            }

        }

        [HttpPost("staff/login1")]
        public async Task<IActionResult> StaffLogin([FromForm] StaffLoginDto staff)
        {
            var result = await _serviceManager.AuthenticationService.Login(staff);
            if (result.Success)
            {
                var response = ApiResponse<StaffLoginDto>.Success(data: (StaffLoginDto)result);
                return new JsonResult(response);
            }
            else
            {
                string errors = "";
                foreach (var msg in result.ErrorMessages) errors += msg;
                var response = ApiResponse<StaffLoginDto>.NotFound(errors);
                return new JsonResult(response);
            }

        }





        [HttpGet("Test/Student/IsAuthenticated")]
        [Authorize(Roles = nameof(Roles.Student))]
        public IActionResult Test()
        {
            return Ok(AuthenticationService.GetUserClaims(HttpContext));
        }

        [HttpGet("Test/Staff/IsAuthenticated")]
        [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Teacher)}")]
        public IActionResult TeststaffAuth()
        {
            return Ok(AuthenticationService.GetUserClaims(HttpContext));
        }

    }
}
