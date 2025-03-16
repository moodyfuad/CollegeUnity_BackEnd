using CollegeUnity.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CollegeUnity.API.Middlerware_Extentions
{
    public static class CustomModelValidationExtension
    {
        public static IServiceCollection ConfigureModelValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value != null && ms.Value.Errors.Count > 0).
                        Select(
                        keyValuePair => $"{keyValuePair.Key} : [ {string.Join(", ", keyValuePair.Value!.Errors.Select(error => error.ErrorMessage))} ]").ToList();

                    var responseObj = ApiResponse<object>.BadRequest(
                        "One or more validation failed",
                        errors);

                    return new BadRequestObjectResult(responseObj);
                };
            });
            return services;
        }
    }
}
