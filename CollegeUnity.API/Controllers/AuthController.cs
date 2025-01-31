using CollegeUnity.Core.Constants;
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
using CollegeUnity.Services;

namespace CollegeUnity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IServiceManager _serviceManager;

        public AuthController(IConfiguration configuration, IServiceManager serviceManager)
        {
            _config = configuration;
            _serviceManager = serviceManager;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup([FromForm]StudentSignUpDto student)
        {
           string resultMsg = await _serviceManager.AuthenticationService.SignUp(student);
            return Ok(resultMsg);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] StudentLoginDto student)
        {
            string token = await _serviceManager.AuthenticationService.Login(student);
            return Ok(token);
            
        }
    }
}
