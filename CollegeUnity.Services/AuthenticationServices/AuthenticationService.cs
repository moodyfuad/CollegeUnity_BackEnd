using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using EmailService;
using EmailService.EmailService;
using EmailService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService : IAuthenticationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _config;
        private readonly IEmailServices _emailServices;

        public AuthenticationService(IRepositoryManager repositoryManager, IConfiguration config, IEmailServices emailServices)
        {
            _repositoryManager = repositoryManager;
            _config = config;
            _emailServices = emailServices;
        }

        public async Task<UserLoginDto> Login(
             UserLoginDto userLoginDto,
             DateTime? expireAt = null)
        {
            switch (userLoginDto)
            {
                // student log in
                case StudentLoginDto studentDto:
                    Student? student = await ValidateStudentCredentials(studentDto);
                    return student == null ?
                        StudentLoginDto.Failed (["Your Id or Password is incorrect"]) :
                        StudentLoginDto.Success(CreateToken(student,expireAt));
                
                // staff log in
                case StaffLoginDto staffDto:
                    Staff? staff = await ValidateStaffCredentials(staffDto);
                    return staff == null ?
                        StaffLoginDto.Failed([ "Your Email or Password is incorrect" ]):
                        StaffLoginDto.Success(CreateToken(staff,expireAt));

                //default
                default: return StaffLoginDto.Failed(["User Type Error"]);
            }

        }

        public async Task<string> SignUp(UserSignUpDto userSignUpDto)
        {
            switch (userSignUpDto)
            {
                case StudentSignUpDto studentDto:
                    return await SignUpStudent(studentDto);

                default: return "error occur!";
            }


        }

        public static UserClaimsDto GetUserClaims(in HttpContext context)
        {
            List<Claim> claims = context.User.Claims.ToList();

            var userRoles = claims.
                Where(c => c.Type == CustomClaimTypes.Role)
               .Select(c => Enum.Parse<Roles>(c.Value))
               .ToList();
            //string role = claims.Single(claim => claim.Type.Equals(CustomClaimTypes.Role))!.Value;

            if (userRoles.Contains(Roles.Student))
            {
                return GetStudentClaims(claims);
            }
            else
            {
                return GetStaffClaims(claims);
            }
        }

        public async Task<Result> SendResetPasswordRequest(string email, Roles role)
        {
            DateTime CodeExpriresAt = DateTime.UtcNow.AddMinutes(5);
            return await _SendResetPasswordRequest(email, role, CodeExpriresAt);
        }
    }
}
