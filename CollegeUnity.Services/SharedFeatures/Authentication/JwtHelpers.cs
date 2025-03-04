using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.AuthenticationServicesDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.SharedFeatures.Authentication
{
    public static class JwtHelpers
    {
        public static string CreateToken(
            in User user,
            in IConfiguration config,
            in DateTime? expireAt = null)
        {
            JwtSecurityToken token = new(
            issuer: config[$"Jwt:{JwtKeys.Issuer}"],
            audience: config[$"Jwt:{JwtKeys.Audience}"],
            signingCredentials: CreateSigningCredentials(in config),

            claims: CreateUserClaims(user),

            expires: expireAt ?? DateTime.Now.AddMonths(3));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string CreateForgetPasswordToken(
            in string email,
            in ForgetPasswordRoles forgetPasswordRoles,
            in IConfiguration _config,
            in DateTime? expireAt = null)
        {
            JwtSecurityToken token = new(
            issuer: _config[$"Jwt:{JwtKeys.Issuer}"],
            audience: _config[$"Jwt:{JwtKeys.Audience}"],
            signingCredentials: CreateSigningCredentials(in _config),

            claims: new List<Claim>()
            {
                new Claim(CustomClaimTypes.Role, forgetPasswordRoles.ToString()),
                new Claim(CustomClaimTypes.Email, email),
            },

            expires: expireAt ?? DateTime.Now.AddMinutes(5));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static UserClaimsDto GetUserClaims(in HttpContext context)
        {
            List<Claim> claims = context.User.Claims.ToList();

            var userRoles = claims.
                Where(c => c.Type == CustomClaimTypes.Role)
               .Select(c => Enum.Parse<Roles>(c.Value))
               .ToList();

            if (userRoles.Contains(Roles.Student))
            {
                return StudentClaimsDto.FromClaims(claims);
            }
            else
            {
                return StaffClaimsDto.MapFromClaims(claims);
            }
        }

        private static SigningCredentials CreateSigningCredentials(in IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[$"Jwt:{JwtKeys.Key}"]!));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

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
                new Claim(CustomClaimTypes.RegistrationID, student.CardId.ToString()),
                //new Claim(CustomClaimTypes.Major, student.Major.ToString()),
                //new Claim(CustomClaimTypes.Level, student.Level.ToString()),
                //new Claim(CustomClaimTypes.AcceptanceType, student.AcceptanceType.ToString()),

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
                //new Claim(CustomClaimTypes.FirstName, user.FirstName),
                //new Claim(CustomClaimTypes.MiddleName, user.MiddleName),
                //new Claim(CustomClaimTypes.LastName, user.LastName),
                new Claim(CustomClaimTypes.FullName, $"{user.FirstName} {user.MiddleName} {user.LastName}"),
                new Claim(CustomClaimTypes.Gender, user.Gender.ToString()),
                //new Claim(CustomClaimTypes.Email, user.Email),
                //new Claim(CustomClaimTypes.BirthDate, user.BirthDate.ToString()),
                //new Claim(CustomClaimTypes.AccountStatus, user.AccountStatus.ToString()),
                //new Claim(CustomClaimTypes.PhoneNumber, user.Phone.ToString()),
            };

            var roles = GetUserRoleNames(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(CustomClaimTypes.RoleName, role));
            }
            claims.AddRange(GetUserRoleClaims(user));
            return claims;
        }

        #region Privous code that change the role name because of the comma (,)
        //private static string GetUserRoleName(User user)
        //{
        //    switch (user)
        //    {
        //        case Student student:
        //            return Roles.Student.AsString();
        //        case Staff staff:
        //            var roles = string.Empty;
        //            foreach (var role in staff.Roles)
        //            {
        //                roles += role.AsString() + ",";
        //            }

        //            return roles;
        //        default: return "No Role";
        //    }
        //}
        #endregion
        private static List<string> GetUserRoleNames(User user)
        {
            switch (user)
            {
                case Student student:
                    return new List<string> { Roles.Student.AsString() };

                case Staff staff:
                    return staff.Roles.Select(role => role.AsString()).ToList();

                default:
                    return new List<string> { "No Role" };
            }
        }

        private static List<Claim> GetUserRoleClaims(User user)
        {
            switch (user)
            {
                case Student:
                    return [new Claim(CustomClaimTypes.Role, Roles.Student.ToString())];
                case Staff staff:
                    List<Claim> roleClaims = [];
                    foreach (var role in staff.Roles)
                    {
                        roleClaims.Add(new Claim(CustomClaimTypes.Role, role.ToString()));
                    }

                    return roleClaims;
                default: return [];
            }
        }
    }
}
