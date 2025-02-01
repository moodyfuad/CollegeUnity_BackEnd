using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService
    {

        private static List<Claim>? CreateUserClaims(in User user)
        {
            switch (user)
            {
                case Student student:
                    return CreateStudentClaims(student);

                case Staff staff:
                    return CreateStaffClaims(staff);

                default: return null;
            }
        }

        private static List<Claim> CreateStudentClaims(Student student)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaimTypes.CardId, student.CardId.ToString()),
                new Claim(CustomClaimTypes.Major, student.Major.ToString()),
                new Claim(CustomClaimTypes.Level, student.Level.ToString()),
                new Claim(CustomClaimTypes.AcceptanceType, student.AcceptanceType.ToString()),

            };
            claims.AddRange(CreateSharedClaims(student));
            return claims;
        }

        private static List<Claim> CreateStaffClaims(Staff staff)
        {
            var claims = new List<Claim>()
            {
                new Claim(CustomClaimTypes.EducationDegree, staff.EducationDegree.ToString()),
            };

            claims.AddRange(CreateSharedClaims(staff));
            return claims;
        }

        private static List<Claim> CreateSharedClaims(User user)
        {
            var claims = new List<Claim>()
            {
            new Claim(CustomClaimTypes.Id, user.Id.ToString()),
            new Claim(CustomClaimTypes.FirstName, user.FirstName),
            new Claim(CustomClaimTypes.MiddleName, user.MiddleName),
            new Claim(CustomClaimTypes.LastName, user.LastName),
            new Claim(CustomClaimTypes.FullName, $"{user.FirstName} {user.MiddleName} {user.LastName}"),
            new Claim(CustomClaimTypes.Gender, user.Gender.ToString()),
            new Claim(CustomClaimTypes.Email, user.Email),
            new Claim(CustomClaimTypes.BirthDate, user.BirthDate.ToString()),
            new Claim(CustomClaimTypes.AccountStatus, user.AccountStatus.ToString()),
            new Claim(CustomClaimTypes.PhoneNumber, user.Phone.ToString()),
            new Claim(CustomClaimTypes.Role, GetUserRole(user)),
            new Claim(CustomClaimTypes.RoleName, GetUserRoleName(user)),
        };
            return claims;
        }

        private static string GetUserRole(User user)
        {
            switch (user)
            {
                case Student student:
                    return Roles.Student.ToString();
                case Staff staff:
                    var roles = string.Empty;
                    foreach (var role in staff.Roles)
                    {
                        roles += role.ToString();
                    }
                    return roles;
                default: return "No Role";
            }
        }

        private static string GetUserRoleName(User user)
        {
            switch (user)
            {
                case Student student:
                    return Roles.Student.AsString();
                case Staff staff:
                    var roles = string.Empty;
                    foreach (var role in staff.Roles)
                    {
                        roles += role.AsString() + ",";
                    }
                    
                    return roles.Remove(-1, 1);
                default: return "No Role";
            }
        }

        private async Task<Student?> ValidateStudentCredentials(StudentLoginDto studentDto)
        {
            Student? student =
                await _repositoryManager.StudentRepository
                .GetByConditionsAsync(
                    std => std.CardId == studentDto.CardId && std.Password == studentDto.Password);

            return student;
        }

        private async Task<Staff?> ValidateStaffCredentials(StaffLoginDto staffDto)
        {
            Staff? staff =
                await _repositoryManager.StaffRepository
                .GetByConditionsAsync(
                    staff => staff.Email == staffDto.Email && staff.Password == staffDto.Password);

            return staff;
        }
    }
}
