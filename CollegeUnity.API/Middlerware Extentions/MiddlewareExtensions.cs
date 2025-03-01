using CollegeUnity.Core.Constants.AuthenticationConstants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Runtime.CompilerServices;
using System.Security.Claims;
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyNames.AdminOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "Admin");
                });

                options.AddPolicy(PolicyNames.DeanOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "Dean");
                });

                options.AddPolicy(PolicyNames.TeacherOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "Teacher");
                });

                options.AddPolicy(PolicyNames.StudentAffairsViceDeanShipOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "StudentAffairsViceDeanShip");
                });

                options.AddPolicy(PolicyNames.RegistrationAdmissionEmployeeOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "RegistrationAdmissionEmployee");
                });

                options.AddPolicy(PolicyNames.HeadOfITDepartmentOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "HeadOfITDepartment");
                });

                options.AddPolicy(PolicyNames.HeadOfCSDepartmentOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "HeadOfCSDepartment");
                });

                options.AddPolicy(PolicyNames.StudentOnly, policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim(CustomClaimTypes.RoleName, "Student");
                });
            });

            return services.AddAuthentication(options => options = authenticationOptions)
                .AddJwtBearer(option => option.TokenValidationParameters = tokenValidationParameters);
        }

        public static void AddSwaggerCustomeGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "CollageUnityApi", Version = "v1" });

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
