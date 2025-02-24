using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures;
using CollegeUnity.Core.Entities;
using EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Authentication
{
    public class LoginFeatures : ILoginFeatures
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IConfiguration _config;

        public LoginFeatures(IRepositoryManager repositoryManager, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
            _config = config;
        }

        public async Task<LoginResultDto> Login(
             UserLoginDto userLoginDto,
             DateTime? expireAt = null)
        {
            switch (userLoginDto)
            {
                // student log in
                case StudentLoginDto studentDto:
                    Student? student = await _ValidateStudentCredentials(studentDto);
                    return student == null ?
                        LoginResultDto.Failed(["Your Id or Password is incorrect"]) :
                        LoginResultDto.Success(JwtHelpers.CreateToken(student,_config , expireAt));

                // staff log in
                case StaffLoginDto staffDto:
                    Staff? staff = await _ValidateStaffCredentials(staffDto);
                    return staff == null ?
                        LoginResultDto.Failed(["Your Email or Password is incorrect"]) :
                        LoginResultDto.Success(JwtHelpers.CreateToken(staff,_config, expireAt));

                //default
                default: return LoginResultDto.Failed(["User Type Error"]);
            }
        }

        private async Task<Student?> _ValidateStudentCredentials(StudentLoginDto studentDto)
        {
            Student? student =
                await _repositoryManager.StudentRepository
                .GetByConditionsAsync(
                    std => std.CardId == studentDto.CardId && std.Password == studentDto.Password);

            return student;
        }

        private async Task<Staff?> _ValidateStaffCredentials(StaffLoginDto staffDto)
        {
            Staff? staff = await _repositoryManager.StaffRepository
                .GetByConditionsAsync(
                    staff =>
                    staff.Email == staffDto.Email &&
                    staff.Password == staffDto.Password);

            return staff;
        }
    }
}
