using CollegeUnity.Core.Constants;
using CollegeUnity.Core.Dtos;
using CollegeUnity.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeUnity.Services
{
    public class JwtServices
    {
        
        public static string CreateToken(
            in JwtUserDto user,
            in IConfiguration _config,
            in DateTime? expireAt = null)
        {
            JwtSecurityToken token = new(
            issuer: _config[$"Jwt:{JwtKeys.Issuer}"],
            audience: _config[$"Jwt:{JwtKeys.Audience}"],
            signingCredentials: CreateSigningCredentials(in _config),
            claims: CreateUserClaims(in user),
            expires: expireAt ?? DateTime.Now.AddMonths(3)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static JwtUserDto GetUserClaims(in HttpContext context)
        {
            
            var claims = context.User.Claims;
            JwtUserDto user = new() 
            {
                Id = Guid.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Id)!.Value),
                FirstName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.FirstName)!.Value,
                MiddleName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.MiddleName)!.Value,
                LastName = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.LastName)!.Value,
                Email = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Email)!.Value,
                Username = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Username)!.Value,
                BirthDate = DateOnly.Parse(claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.BirthDate)!.Value),
                Gender = (Gender)Enum.Parse(typeof(Gender),claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Gender)!.Value),
                Role = (Roles)Enum.Parse(typeof(Roles),claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Role)!.Value),
                Position = claims.FirstOrDefault(claim => claim.Type == CustomClaimTypes.Position)!.Value,
                
            };

            return user;
        }

        private static SigningCredentials CreateSigningCredentials(in IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[$"Jwt:{JwtKeys.Key}"]!));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private static List<Claim> CreateUserClaims(in JwtUserDto user)
        {
            var claims = new List<Claim>()
        {
            new Claim(CustomClaimTypes.Id, user.Id.ToString()),
            new Claim(CustomClaimTypes.FirstName, user.FirstName),
            new Claim(CustomClaimTypes.MiddleName, user.MiddleName),
            new Claim(CustomClaimTypes.LastName, user.LastName),
            new Claim(CustomClaimTypes.FullName, user.FullName),
            new Claim(CustomClaimTypes.Role, user.Role.ToString()),
            new Claim(CustomClaimTypes.Username, user.Username),
            new Claim(CustomClaimTypes.Gender, user.Gender.ToString()),
            new Claim(CustomClaimTypes.Email, user.Email),
            new Claim(CustomClaimTypes.BirthDate, user.BirthDate.ToString()),
            new Claim(CustomClaimTypes.Position, user.Position.ToString()),
        };
            return claims;
        }
    }
}
