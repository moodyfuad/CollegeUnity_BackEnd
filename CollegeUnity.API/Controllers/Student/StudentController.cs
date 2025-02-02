using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeUnity.API.Controllers.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public StudentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpPost("SignUp")]
        public async Task<IActionResult> Signup([FromForm] StudentSignUpDto student)
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