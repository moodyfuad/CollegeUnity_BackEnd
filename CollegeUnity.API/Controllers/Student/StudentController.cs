using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        
        [HttpPost("Search")]
        public async Task<IActionResult> StudentSearch([FromQuery] StudentSearchParameters searchParameters)
        {
            try
            {
                var result = await _serviceManager.StudentServices.GetStudentsAsync(searchParameters);


                if (!result.Any())
                    return Ok(ApiResponse<List<Core.Entities.Student>>.NotFound());

                if (result.Count().Equals(1))
                    return Ok(ApiResponse<Core.Entities.Student>.Success(data: result.FirstOrDefault()!));

                if (result.Count() > 1)
                    return Ok(ApiResponse<List<Core.Entities.Student>>.Success(message: $"[{result.Count()}] records fetched.",data: result.ToList()));
                else 
                    return Ok(ApiResponse<string>.InternalServerError(errors :["error fitching students"]));

            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.InternalServerError(errors: [ex.Message]));

            }


        }
    }
}