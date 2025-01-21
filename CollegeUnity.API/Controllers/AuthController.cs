using CollegeUnity.Services;
using CollegeUnity.Core.Constants;
using CollegeUnity.Core.DomainModels;
using CollegeUnity.Core.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeUnity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _config;

        public AuthController(IConfiguration configuration)
        {
            _config = configuration;
        }

        [Authorize(Roles = nameof(Roles.Student))]
        [HttpGet("AreYouLoggedIn")]
        public IActionResult Check()
        {
            return Ok(JwtServices.GetUserClaims(HttpContext));
        }

        [HttpPost("Login")]
        public IActionResult Login([FromForm] UserLoginDto user)
        {
            // check if the user exist
            //.....
            if (user == null || user.Username != "moody" || user.Password != "1234")
            {
                return NotFound();
            }
            //
            //Get the user info
            // ex 
            JwtUserDto foundUser = JwtUserDto.DefaultUser();

            //
            return Ok(new { token = JwtServices.CreateToken(foundUser, _config) });
        }

    }
}
