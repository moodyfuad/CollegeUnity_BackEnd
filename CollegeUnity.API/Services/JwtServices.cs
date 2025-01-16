using CollegeUnity.Core.Constants;
using CollegeUnity.Core.DomainModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeUnity.API.Services
{
    public class JwtServices
    {
        public static string CreateToken(
            in UserModel user,
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

        private static SigningCredentials CreateSigningCredentials(in IConfiguration _config)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[$"Jwt:{JwtKeys.Key}"]!));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }

        private static List<Claim> CreateUserClaims(in UserModel user)
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
        };
            return claims;
        }
    }
}
