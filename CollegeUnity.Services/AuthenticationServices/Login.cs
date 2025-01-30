using CollegeUnity.Core.Constants;
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
       
        private static StaffClaimsDto GetStaffClaims(HttpContext context)
        {

            var claims = context.User.Claims;
            StaffClaimsDto staff = new StaffClaimsDto()
            {
                Id = int.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),
                FirstName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FirstName)!.Value,
                MiddleName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.MiddleName)!.Value,
                LastName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.LastName)!.Value,
                Email = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Email)!.Value,
                BirthDate = DateOnly.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.BirthDate)!.Value),
                Gender = (Gender)Enum.Parse(typeof(Gender), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender)!.Value),
                Role = (Roles)Enum.Parse(typeof(Roles), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Role)!.Value),
                AccountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AccountStatus)!.Value),
                Phone = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.PhoneNumber)!.Value,
                Password = "",
                ConfirmPassword = "",


                //staff
                EducationDegree = (EducationDegree)Enum.Parse(typeof(EducationDegree), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.EducationDegree)!.Value),
            };
            return staff;

        }


        private static List<Claim> CreateUserClaims(in User user)
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
    }
}
