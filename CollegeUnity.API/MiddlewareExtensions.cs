using CollegeUnity.Core.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace CollegeUnity.API
{
    public static class MiddlewareExtensions
    {

        public static AuthenticationBuilder AddCustomJwtAuthentication(this IServiceCollection services, IConfiguration _config) 
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = _config[$"Jwt:{JwtKeys.Issuer}"],
                ValidateAudience = true,
                ValidAudience = _config[$"Jwt:{JwtKeys.Audience}"],
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config[$"Jwt:{JwtKeys.Key}"]!)),
            };
            AuthenticationOptions authenticationOptions = new()
            {
                DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme,
                DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme,
            };

            return services.AddAuthentication(options => options = authenticationOptions)
                .AddJwtBearer(option => option.TokenValidationParameters = tokenValidationParameters);
        }
    }
}
