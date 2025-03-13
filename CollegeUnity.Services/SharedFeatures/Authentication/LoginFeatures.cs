using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using EmailService;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                    if (student == null)
                    {
                        return LoginResultDto.Failed(["Your Id or Password is incorrect"]);
                    }

                    return student.AccountStatus switch
                    {
                        AccountStatus.Active => LoginResultDto.Success(JwtHelpers.CreateToken(student, _config, expireAt)),

                        AccountStatus.Deactive => LoginResultDto.Failed(["Your Account is Deactivated.",
                            $"Due to {student.AccountStatusReason}"]),

                        AccountStatus.Denied => LoginResultDto.Failed(
                            ["Your Account is not Rejected by the registration department.",
                            $"Rejection Reason: {student.AccountStatusReason}",
                            "Please Sign up again using your correct information or Visit the registration department for review."]),

                        AccountStatus.Waiting => LoginResultDto.Success(JwtHelpers.CreateToken(student, _config, expireAt)),
                        //LoginResultDto.Failed(["Your Account is not Activated Yet.",
                        //    "Waiting for the registration department to accept your sign up request."]),

                        _ => LoginResultDto.Failed("one or more errors please try again later"),
                    };

                // staff log in
                case StaffLoginDto staffDto:
                    Staff? staff = await _ValidateStaffCredentials(staffDto);
                    if (staff == null)
                    {
                        return LoginResultDto.Failed(["Your Email or Password is incorrect"]);
                    }

                    return staff.AccountStatus switch
                    {
                        AccountStatus.Active => LoginResultDto.Success(JwtHelpers.CreateToken(staff, _config, expireAt)),

                        AccountStatus.Deactive => LoginResultDto.Failed(["Your Account is Deactivated.",
                            $"Due to {staff.AccountStatusReason}"]),

                        AccountStatus.Denied => LoginResultDto.Failed(
                            ["your account is denied which is not correct please contact the application Admin"]),

                        AccountStatus.Waiting => LoginResultDto.Success(JwtHelpers.CreateToken(staff, _config, expireAt)),
                        //LoginResultDto.Failed(["your account is in waiting state which is not correct please contact the application Admin"]),

                        _ => LoginResultDto.Failed("one or more errors please try again later"),
                    };

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
