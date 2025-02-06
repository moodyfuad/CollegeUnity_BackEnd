using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.StudentServiceDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Helpers;
using EmailService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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

                var meta = new
                {
                    PageNumber = result.CurrentPage,
                    TotalPages = result.TotalPages,
                    PageSize = result.PageSize,
                    HasPrevious = result.HasPrevious,
                    HasNext = result.HasNext,
                };

                Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(meta));
                if (!result.Any())
                    return Ok(ApiResponse<List<Core.Entities.Student>>.NotFound());

                if (result.Count.Equals(1))
                    return Ok(ApiResponse<Core.Entities.Student>.Success(data: result.FirstOrDefault()!));

                if (result.Count > 1)
                    return Ok(ApiResponse<PagedList<Core.Entities.Student>>.Success(message: $"[{result.Count}] records fetched.",data: result));
                else 
                    return Ok(ApiResponse<string>.InternalServerError(errors :["error fetching students"]));

            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<string>.InternalServerError(errors: [ex.Message]));

            }


        }

        [HttpPost("{email}/PasswordResetCode")]
        public async Task<IActionResult> RequestPasswordResetCode(string email)
        {
            var result = await this._serviceManager.AuthenticationService.SendResetPasswordRequest(email, Roles.Student);
            var response =
            result.IsSuccess ?
                 ApiResponse<Result>.Success(result)
            :
                 ApiResponse<Result>.NotFound(message: result.Message);

            this.HttpContext.Response.StatusCode = response.StatusCode;
            return new JsonResult(response);
        }

        [HttpPost("{email}/PasswordResetCode/check{ResetCode}")]
        [Authorize(Roles = $"{nameof(AuthenticationRoles.ForgotPassword)}")]
        public async Task<IActionResult> CheckPasswordResetCode(string email, string resetCode)
        {
            var result = await this._serviceManager.StudentServices.CheckResetPasswordCode(email, resetCode);
            var response =
            result ?
                 ApiResponse<string>.Success($"Redirect to [{this.HttpContext.Request.PathBase}/PasswordReset/ResetCode/newPassword/Reset")
            :
                 ApiResponse<string>.BadRequest("Invalid Code");

            this.HttpContext.Response.StatusCode = response.StatusCode;
            return new JsonResult(response);
        }

        [Authorize(Roles = $"{nameof(AuthenticationRoles.ForgotPassword)}")]
        [HttpPost("{email}/PasswordReset/ResetCode/{resetCode}/newPassword")]
        public async Task<IActionResult> ResetPassword(string email, string resetCode, [FromForm] string newPassword)
        {
            var result = await this._serviceManager.StudentServices.ResetPassword(email, resetCode, newPassword);
            var response =
            result ?
                 ApiResponse<string>.Success(null,"Password Reset Successfully")
            :
                 ApiResponse<string>.InternalServerError(errors: ["failed resetting the password"]);

            HttpContext.Response.StatusCode = response.StatusCode;
            return new JsonResult( response);
        }
    }
}