//using AutoMapper;
using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.Services_Contract.ServiceAbstraction;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
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

        public AuthenticationService(IRepositoryManager repositoryManager, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
            _config = config;
            //_mapper = mapper;
        }

        public async Task<string> Login(
             UserLoginDto userLoginDto,
             DateTime? expireAt = null)
        {
            switch (userLoginDto)
            {
                // student log in
                case StudentLoginDto studentDto:
                    Student? student = await ValidateStudentCredentials(studentDto);
                    return student == null ?
                        "Your Id or Password is incorrect" : CreateToken(student,expireAt);
                
                // staff log in
                case StaffLoginDto staffDto:
                    Staff? staff = await ValidateStaffCredentials(staffDto);
                    return staff == null ?
                        "Your Email or Password is incorrect" 
                        : CreateToken(staff,expireAt);

                //default
                default: return "User Type Error";
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
    }
}
