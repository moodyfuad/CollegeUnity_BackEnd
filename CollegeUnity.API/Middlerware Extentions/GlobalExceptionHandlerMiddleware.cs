using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Entities.Errors;
using System.Text.Json;

namespace CollegeUnity.API.Middlerware_Extentions
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            ApiResponse<object> response;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    response = ApiResponse<object>.Unauthorized(exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    break;
                case ArgumentException:
                    response = ApiResponse<object>.BadRequest(exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case InvalidOperationException:
                    response = ApiResponse<object>.BadRequest(exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    response = ApiResponse<object>.InternalServerError(exception.Message);
                    httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

}
