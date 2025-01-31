//using AutoMapper;
using CollegeUnity.Contract;
using CollegeUnity.Core.Constants;
using CollegeUnity.Core.Dtos;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using CollegeUnity.Services.ServiceAbstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
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
        //private readonly IMapper _mapper;

        public AuthenticationService(IRepositoryManager repositoryManager, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
            _config = config;
            //_mapper = mapper;
        }

        public async Task<string> Login(
             UserLoginDto userLoginDto,
             //IConfiguration _config,
             DateTime? expireAt = null)
        {
            switch (userLoginDto)
            {
                // student log in
                case StudentLoginDto studentDto:
                    Student? student = await ValidateStudentCredentials(expireAt, studentDto);
                    return student == null ?
                        "Your Id or Password is incorrect" 
                        : CreateToken(student);

                //staff log in

                //default
                default: return "User Type Error";
            }

        }

        private async Task<Student?> ValidateStudentCredentials(DateTime? expireAt, StudentLoginDto studentDto)
        {
            Student? student = 
                await _repositoryManager.StudentRepository
                .GetByConditionsAsync(
                    std => std.CardId == studentDto.CardId && std.Password == studentDto.Password);

            return student;
        }

        public async Task<string> SignUp(UserSignUpDto userSignUpDto)
        {
            switch (userSignUpDto)
            {
                case StudentSignUpDto studentDto:
                    return await SignUpStudent(studentDto);

                //case StaffSignUpDto StaffDto:
                //    return await SignUpStaff(staffDto);

                default: return "error occur!";
            }


        }

        public static AuthenticationUserDto GetUserClaims(in HttpContext context)
        {
            var claims = context.User.Claims;

            Roles userRole = (Roles)Enum.Parse(typeof(Roles), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Role)!.Value);
            if (userRole == Roles.Student)
            {
                return GetStudentClaims(context);
            }
            else
            {
                return GetStaffClaims(context);
            }
        }
    }
}
