using CollegeUnity.Core.Entities.Errors;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CollegeUnity.API.Middlerware_Extentions
{
    public static class ExceptionMiddlerwareExtentions
    {
        public static void ConfigureCustomeExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
