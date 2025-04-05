using CollegeUnity.API.Filters;
using CollegeUnity.Core.Constants.AuthenticationConstants;
using CollegeUnity.Core.Dtos.ResponseDto;
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
using System.Text.Json;

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

            var jwtBearerEvents = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse(); // Prevent default behavior

                    ApiResponse<string> response = ApiResponse<string>.Unauthorized();

                    context.Response.StatusCode = response.StatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            };
            return services.AddAuthentication(options => options = authenticationOptions)
                .AddJwtBearer(
                option =>
                {
                    option.TokenValidationParameters = tokenValidationParameters;
                    option.Events = jwtBearerEvents;
                }
                );
        }

        public static void AddSwaggerCustomeGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "CollageUnityApi", Version = "v1" });

                s.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description = "Enter your JWT token below:\n" +
                                      "1. Enter the token without 'Bearer ' keyword.\n\n" +
                                      "2. Click 'Authorize' to apply authentication.\n\n",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT"
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
                            //Scheme = "Http",
                            Scheme = SecuritySchemeType.Http.ToString(),
                            BearerFormat = "JWT",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                s.OperationFilter<AddSwaggerDocOperationFilter>();
            });
        }
    }
}
