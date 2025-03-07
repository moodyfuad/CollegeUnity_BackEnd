using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.ResponseDto
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public ApiResponse(int statusCode, bool isSuccess, string message, int? pageNumber, int? pageSize,T data = default, List<string> errors = null)
        {
            StatusCode = statusCode;
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Errors = errors ?? new List<string>();
        }

        public static ApiResponse<T?> Success(T? data, string message = "Request completed successfully.", int pageNumber = 1, int pageSize = 10)
        {
            return new ApiResponse<T?>(200, true, message, pageNumber, pageSize, data);
        }

        public static ApiResponse<T?> Created(T? data, string message = "Resource created successfully.", int pageNumber = 1, int pageSize = 10)
        {
            return new ApiResponse<T?>(201, true, message, pageNumber, pageSize, data);
        }

        public static ApiResponse<T?> BadRequest(string message = "Invalid request.", List<string> errors = null)
        {
            return new ApiResponse<T?>(400, false, message, null, null, default, errors);
        }

        public static ApiResponse<T?> NotFound(string message = "Resource not found.")
        {
            return new ApiResponse<T?>(404, false, message, null, null);
        }

        public static ApiResponse<T> Unauthorized(string message = "Unauthorized access.")
        {
            return new ApiResponse<T>(401, false, message, null, null);
        }

        public static ApiResponse<T> InternalServerError(string message = "An unexpected error occurred.", List<string> errors = null)
        {
            return new ApiResponse<T>(500, false, message, null, null, default, errors);
        }
    }
}


