using CollegeUnity.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CollegeUnity.API.Filters
{
    public class AddSwaggerDocOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var returnType = GetReturnType(context.MethodInfo);
            if (returnType == null || returnType == typeof(void))
                return;

            var wrappedType = returnType;
            var schema = context.SchemaGenerator.GenerateSchema(wrappedType, context.SchemaRepository);

            var response200 = AddSuccess200Response(schema);

            // Add headers if it's ApiResponse<PagedList<T>>
            if (IsPagedListResponse(returnType))
            {
                response200.Headers = new Dictionary<string, OpenApiHeader>
                {
                    { "X-Pagination", CreatePaginationHeader(context) }
                };
            }

            operation.Responses["200"] = response200;

            operation.Responses.TryAdd("400", AddErrorResponses(context, 400, "Bad Request", "Invalid request"));

            // 401 Unauthorized
            operation.Responses.TryAdd("401", AddErrorResponses(context, 401, "Unauthorized", "Authentication required"));

            // 403 Forbidden
            operation.Responses.TryAdd("403", AddErrorResponses(context, 403, "Forbidden", "Access denied"));

            // 404 Not Found
            operation.Responses.TryAdd("404", AddErrorResponses(context, 404, "Not Found", "Resource not found"));

            // 500 Internal Server Error
            operation.Responses.TryAdd(
                "500",
                AddErrorResponses(context, 500, "Internal Server Error", "An unexpected error occurred"));
        }

        private OpenApiResponse AddSuccess200Response(OpenApiSchema schema)
        {
            var response = new OpenApiResponse
            {
                Description = "Success",
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/json"] = new OpenApiMediaType
                    {
                        Schema = schema
                    }
                }
            };
            return response;
        }

        private OpenApiResponse AddErrorResponses(
            OperationFilterContext context,
            int statusCode,
            string errorDescription,
            string errorContent = "")
        {
            var response = new OpenApiResponse
            {
                Description = errorDescription,
                Content = GetErrorContent(context, statusCode, errorContent)
            };
            return response;
        }

        private static Dictionary<string, OpenApiMediaType> GetErrorContent(OperationFilterContext context, int statusCode, string defaultMessage)
        {
            var errorType = typeof(ApiResponse<object?>);
            var schema = context.SchemaGenerator.GenerateSchema(errorType, context.SchemaRepository);

            return new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = schema,
                    Example = OpenApiAnyFactory.CreateFromJson(
                        $@"{{
                            ""isSuccess"":false,
                            ""statusCode"":{statusCode},
                            ""message"":""{defaultMessage}"",
                            ""data"":null,
                            ""errors"":[""string1"",""string2""]
                            }}")
                }
            };
        }

        private static Type? GetReturnType(MethodInfo methodInfo)
        {
            var returnType = methodInfo.ReturnType;

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnType = returnType.GetGenericArguments()[0];
            }

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ActionResult<>))
            {
                returnType = returnType.GetGenericArguments()[0];
            }

            if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                return returnType;
            }

            return returnType.GetGenericArguments().FirstOrDefault(t => t == typeof(ApiResponse<>));
        }

        private static bool IsPagedListResponse(Type returnType)
        {
            if (!returnType.IsGenericType) return false;

            var innerType = returnType.GetGenericArguments()[0];

            if (!innerType.IsGenericType) return false;

            return innerType.GetGenericTypeDefinition().Name.StartsWith("PagedList");
        }

        private static OpenApiHeader CreateHeader(string description, string type)
        {
            return new OpenApiHeader
            {
                Description = description,
                Schema = new OpenApiSchema { Type = type }
            };
        }

        private static OpenApiHeader CreatePaginationHeader(OperationFilterContext context)
        {
            var meta = new
            {
                PageNumber = 1,
                TotalPages = 4,
                PageSize = 10,
                HasPrevious = false,
                HasNext = true,
            };
            var errorType = meta.GetType();
            var schema = context.SchemaGenerator.GenerateSchema(errorType, context.SchemaRepository);
            return new OpenApiHeader
            {
                Description = "Pagination metadata",
                Schema = schema,
            };
        }
    }
}
