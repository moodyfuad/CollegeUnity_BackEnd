using CollegeUnity.Contract.Services_Contract;
using CollegeUnity.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CollegeUnity.API.Filters
{
    /// <summary>
    /// this class gets the entities ids ex: postId, userId
    /// and checks if it exists in the database or not
    /// base on that it returns the result
    /// </summary>
    public class ValidateEntityExistAttribute : TypeFilterAttribute
    {
        public ValidateEntityExistAttribute(params string[] IdNames)
            : base(typeof(ValidateExistActionFilter))
        {
            Arguments = new object[] { IdNames };
        }

        private class ValidateExistActionFilter : ActionFilterAttribute
        {
            private readonly IServiceManager _serviceManager;
            private readonly string[] _idNames;

            public ValidateExistActionFilter(
                IServiceManager serviceManager,
                params string[] idNames)
            {
                _serviceManager = serviceManager;
                _idNames = idNames ?? [];
            }

            public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var routeData = context.RouteData.Values;
                var queryData = context.HttpContext.Request.Query;

                bool found = true;
                foreach (string idName in _idNames)
                {
                    bool isInRoute = routeData.ContainsKey(idName);
                    bool isInQuery = queryData.ContainsKey(idName);
                    found = isInRoute || isInQuery;
                    if (isInRoute)
                    {
                        int id = GetIdFromRoute(idName, routeData);
                        bool exist = await CallIsExistGenericAsync(id, idName);
                        found = exist;
                    }
                    else if (isInQuery)
                    {
                        int id = GetIdFromQuery(idName, queryData);
                        bool exist = await CallIsExistGenericAsync(id, idName);
                        found = exist;
                    }

                    if (!found)
                    {
                        ReturnNotFoundResult(ref context);
                        break;
                    }
                }

                if (found)
                {
                    await next();
                }
            }

            private static int GetIdFromRoute(string propertyIdName, RouteValueDictionary routeData)
            {
                var vla = routeData.GetValueOrDefault(propertyIdName, 0);
                bool parseResult = int.TryParse(vla!.ToString(), out int id);

                return parseResult ? id : 0;
            }

            private static int GetIdFromQuery(string propertyIdName, IQueryCollection queryData)
            {
                var vla = queryData.FirstOrDefault(kvp => kvp.Key.Equals(propertyIdName)).Value;
                bool parseResult = int.TryParse(vla!.ToString(), out int id);

                return parseResult ? id : 0;
            }

            private static void ReturnNotFoundResult(ref ActionExecutingContext context, object? obj = null)
            {
                context.Result = new JsonResult(new NotFoundObjectResult(obj ?? "Resource not found"));
            }

            private async Task<bool> CallIsExistGenericAsync(int id, string propertyName)
            {
                // here add the condition to customize the type
                GetEntityType(propertyName, out Type type);

                MethodInfo? method = typeof(ValidateExistActionFilter)
                    .GetMethod(nameof(IsExist), BindingFlags.NonPublic | BindingFlags.Instance)?
                    .MakeGenericMethod(type);

                if (method != null)
                {
                    // Invoke the method and await the returned Task<bool>
                    Task<bool>? task = (Task<bool>?)method.Invoke(this, new object[] { id });

                    if (task != null)
                    {
                        return await task;
                    }
                }

                return false;
            }

            private async Task<bool> IsExist<T>(int id)
               where T : class
            {
                var entity = await _serviceManager.IsExist<T>(id);

                return entity != null;
            }

            private void GetEntityType(string propertyName, out Type type)
            {
                string name = propertyName.ToLower();
                type =
                    name.Contains("postid") ? typeof(Post) :
                    name.Contains("userid") ? typeof(User) :
                    name.Contains("voteid") ? typeof(PostVote) :
                    name.Contains("commentid") ? typeof(PostComment) :
                    name.Contains("staffId") ? typeof(Staff) :
                    name.Contains("studentId") ? typeof(Student) :
                    typeof(User);
            }
        }
    }
}
