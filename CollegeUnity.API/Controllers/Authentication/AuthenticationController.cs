using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CollegeUnity.Core.Enums;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Services.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.ForgetPasswordFeatures;
using CollegeUnity.Contract.StudentFeatures.Account;

namespace CollegeUnity.API.Controllers.Authentication
{
    [Route("api")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginFeatures _loginFeature;
        private readonly IForgetPasswordFeatures _forgetPasswordFeatures;
        private readonly ISignUpFeatures _signUpFeatures;

        public AuthenticationController(
            IConfiguration configuration,
            ILoginFeatures loginFeature,
            IForgetPasswordFeatures forgetPasswordFeatures,
            ISignUpFeatures signUpFeatures)
        {
            _config = configuration;
            _loginFeature = loginFeature;
            _forgetPasswordFeatures = forgetPasswordFeatures;
            _signUpFeatures = signUpFeatures;
        }

        [HttpPost("student/login")]
        public async Task<IActionResult> StudentLogin([FromForm] StudentLoginDto student)
        {
            var result = await _loginFeature.Login(student);
            if (result.IsSuccess)
            {
                var response = ApiResponse<string>.Success(data: result.Token);
                return new JsonResult(response);
            }
            else
            {
                var response = ApiResponse<string>.BadRequest(errors: result.ErrorMessages?.ToList() ?? []);
                return new JsonResult(response);
            }
        }

        [HttpPost("student/SignUp")]
        public async Task<IActionResult> StudentSignUp([FromForm] StudentSignUpDto student)
        {
            var result = await _signUpFeatures.SignUpStudent(student);

            return new JsonResult(result);
        }

        [HttpPost("staff/login")]
        public async Task<IActionResult> StaffLogin([FromForm] StaffLoginDto staff)
        {
            var result = await _loginFeature.Login(staff);
            if (result.IsSuccess)
            {
                var response = ApiResponse<LoginResultDto>.Success(data: result);
                return new JsonResult(response);
            }
            else
            {
                string errors = string.Empty;
                foreach (var msg in result.ErrorMessages) errors += msg;

                var response = ApiResponse<LoginResultDto>.NotFound(errors);
                return new JsonResult(response);
            }
        }

        [HttpPost("ResetPassword/Code/Send{email}")]
        public async Task<IActionResult> SendResetVerificationCode(string email)
        {
            var result = await _forgetPasswordFeatures.SendResetPasswordCode(email);

            var response = result.IsSuccess ?
                  ApiResponse<ForgetPasswordFeatureResultDto>.Success(result, result.Message) :
                  ApiResponse<ForgetPasswordFeatureResultDto>.BadRequest(result.Message);

            return new JsonResult(response);
        }

        [HttpPost("ResetPassword/Code/Validate{code}")]
        [Authorize(Roles = nameof(ForgetPasswordRoles.CodeSent))]
        public async Task<IActionResult> ValidateVerificationCode(string code)
        {
            string email = this.HttpContext.User.FindFirst(CustomClaimTypes.Email)?.Value ?? string.Empty;

            var result = await this._forgetPasswordFeatures.ValidateVerificationCode(email, code);

            var response = result.IsSuccess ?
                  ApiResponse<ForgetPasswordFeatureResultDto>.Success(result, result.Message) :
                  ApiResponse<ForgetPasswordFeatureResultDto>.BadRequest(result.Message);

            return new JsonResult(response);
        }

        [Authorize(Roles = nameof(ForgetPasswordRoles.ResetAllowed))]
        [HttpPost("ResetPassword/{newPassword}")]
        public async Task<IActionResult> ResetPassword(string newPassword)
        {
            string email = this.HttpContext.User.FindFirst(CustomClaimTypes.Email)?.Value ?? string.Empty;

            var result = await this._forgetPasswordFeatures.ResetPassword(email, newPassword);

            var response = result.IsSuccess ?
                  ApiResponse<ForgetPasswordFeatureResultDto>.Success(result, result.Message) :
                  ApiResponse<ForgetPasswordFeatureResultDto>.BadRequest(result.Message);

            return new JsonResult(response);
        }

        [HttpGet("Test/Student/IsAuthenticated")]
        [Authorize(Roles = nameof(Roles.Student))]
        public IActionResult Test()
        {
            return Ok(JwtHelpers.GetUserClaims(HttpContext));
        }

        [HttpGet("Test/Staff/IsAuthenticated")]
        [Authorize(Roles = $"{nameof(Roles.Admin)},{nameof(Roles.Teacher)}")]
        public IActionResult TeststaffAuth()
        {
            return Ok(JwtHelpers.GetUserClaims(HttpContext));
        }

    }
}
