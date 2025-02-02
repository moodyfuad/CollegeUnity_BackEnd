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
