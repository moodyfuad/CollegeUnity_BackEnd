using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Entities;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService
    {
        private string CreateToken(
            in User user,
            in DateTime? expireAt = null)
        {
            JwtSecurityToken token = new(
            issuer: _config[$"Jwt:{JwtKeys.Issuer}"],
            audience: _config[$"Jwt:{JwtKeys.Audience}"],
            signingCredentials: CreateSigningCredentials(in _config),

            claims: CreateUserClaims(user),

            expires: expireAt ?? DateTime.Now.AddMonths(3)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static SigningCredentials CreateSigningCredentials(in IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[$"Jwt:{JwtKeys.Key}"]!));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
       
       
        private static StudentClaimsDto GetStudentClaims(IEnumerable<Claim> claims)
        {
            //var claims = context.User.Claims;
            StudentClaimsDto student = StudentClaimsDto.FromClaims(claims);
               
            return student;
        }

        private static StaffClaimsDto GetStaffClaims(IEnumerable<Claim> claims)
        {

            //var claims = context.User.Claims;
            StaffClaimsDto staff = StaffClaimsDto.MapFromClaims(claims);

            return staff;

        }


    }
}
