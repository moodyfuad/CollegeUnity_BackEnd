using CollegeUnity.API.Services;
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

        [Authorize(AuthenticationSchemes =($"{JwtBearerDefaults.AuthenticationScheme}"))]
        [HttpGet("AreYouLoggedIn")]
        public IActionResult Check()
        {
            return Ok(new
            {
                Id = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value,
                Role = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Role)!.Value,
                BirthDate = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.BirthDate)!.Value,
                Full_Name = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FullName)!.Value,
                Email = HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Email)!.Value,

            });
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
            UserModel foundUser = UserModel.DefaultUser();

            //
            return Ok(new { token = JwtServices.CreateToken(foundUser, _config) });
        }

    }
}
