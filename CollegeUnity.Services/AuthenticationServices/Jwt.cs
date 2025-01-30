using CollegeUnity.Core.Constants;
using CollegeUnity.Core.Dtos.AuthenticationDtos;
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

namespace CollegeUnity.Services.AuthenticationServices
{
    public partial class AuthenticationService
    {
        private string CreateToken(
            in User user,
            //in IConfiguration _config,
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


        

       
       

        

        private static StudentClaimsDto GetStudentClaims(HttpContext context)
        {
            var claims = context.User.Claims;
            StudentClaimsDto student = new StudentClaimsDto()
            {
                Id = int.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),
                FirstName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FirstName)!.Value,
                MiddleName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.MiddleName)!.Value,
                LastName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.LastName)!.Value,
                Phone = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.PhoneNumber)!.Value,
                Email = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Email)!.Value,
                BirthDate = DateOnly.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.BirthDate)!.Value),
                Gender = (Gender)Enum.Parse(typeof(Gender), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender)!.Value),
                Role = (Roles)Enum.Parse(typeof(Roles), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Role)!.Value),
                //student

                CardId = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.CardId)!.Value,
                AcceptanceType = (AcceptanceType)Enum.Parse(typeof(AcceptanceType), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AcceptanceType)!.Value),
                AccountStatus = (AccountStatus)Enum.Parse(typeof(AccountStatus), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.AccountStatus)!.Value),
                Major = (Major)Enum.Parse(typeof(Major), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Major)!.Value),
                Level = (Level)Enum.Parse(typeof(Level), claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Level)!.Value),
                Password = "",
                ConfirmPassword = ""
            };
            return student;
        }

    }
}
