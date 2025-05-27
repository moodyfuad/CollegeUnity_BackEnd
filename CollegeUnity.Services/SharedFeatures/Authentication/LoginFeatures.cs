using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Contract.SharedFeatures.Authentication;
using CollegeUnity.Core.CustomExceptions;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures;
using CollegeUnity.Core.Dtos.StudentFeatures;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using EmailService;
using Microsoft.AspNetCore.Authorization;
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
                        throw new KeyNotFoundException("Your Id or Password is incorrect");
                    }

                    return student.AccountStatus switch
                    {
                        AccountStatus.Active => LoginResultDto.Success(JwtHelpers.CreateToken(student, _config, expireAt)),

                        AccountStatus.Deactive => throw new ForbiddenException(
                            "Access Forbidden",
                            [$"Your Account is Deactivated.\n Due to {student.AccountStatusReason}"]),

                        AccountStatus.Denied => throw new ForbiddenException(
                             "Access Forbidden",
                            [$"Your Account is Rejected by the registration department.",
                                 "Rejection Reason: {student.AccountStatusReason}.",
                                 "Please Sign up again using your correct information or Visit the registration department for review."]),

                        AccountStatus.Waiting => throw new UnauthorizedAccessException("Your Account is not Activated Yet, Waiting for the registration department to accept your sign up request."),

                        _ => throw new Exception("one or more errors please try again later"),
                    };

                // staff log in
                case StaffLoginDto staffDto:
                    Staff? staff = await _ValidateStaffCredentials(staffDto);
                    if (staff == null)
                    {
                        throw new KeyNotFoundException("Your Id or Password is incorrect");
                    }

                    return staff.AccountStatus switch
                    {
                        AccountStatus.Active => LoginResultDto.Success(JwtHelpers.CreateToken(staff, _config, expireAt)),

                        AccountStatus.Deactive => throw new ForbiddenException("Your Account is Deactivated.", [$"Due to {staff.AccountStatusReason}"]),

                        AccountStatus.Denied => throw new ForbiddenException(
                            "Account Is Denied",
                        ["which is not correct please contact the application Admin"]),

                        AccountStatus.Waiting => throw new ForbiddenException("Account Is In Waiting State", ["which is not correct please contact the application Admin"]),

                        _ => throw new Exception("one or more errors please try again later"),
                    };

                default: throw new Exception("Student Type Error");
            }
        }

        private async Task<Student?> _ValidateStudentCredentials(StudentLoginDto studentDto)
        {
            Student? student =
                await _repositoryManager.StudentRepository
                .GetByConditionsAsync(
                    std => std.CardId == studentDto.CardId);
            if (student is not null && PasswordHasherHelper.VerifyPassword(student, studentDto.Password ?? string.Empty))
            {
                return student;
            }

            return null;
        }

        private async Task<Staff?> _ValidateStaffCredentials(StaffLoginDto staffDto)
        {
            Staff? staff = await _repositoryManager.StaffRepository
                .GetByConditionsAsync(
                    staff =>
                    staff.Email == staffDto.Email);
            if (staff is not null && PasswordHasherHelper.VerifyPassword(staff, staffDto.Password ?? string.Empty))
            {
                return staff;
            }

            return null;
        }

        public async Task<ApiResponse<string?>> AcceptWaitingStudent(string cardId)
        {
            var student = await _repositoryManager.StudentRepository.GetByConditionsAsync(
                s =>
                    s.CardId.Equals(cardId) &&
                    s.AccountStatus == AccountStatus.Waiting);

            if (student is null)
            {
                return ApiResponse<string>.NotFound();
            }

            student.AccountStatus = AccountStatus.Active;
            await _repositoryManager.SaveChangesAsync();

            return ApiResponse<string?>.Success(null, "Student Account Activated.");
        }
    }
}
