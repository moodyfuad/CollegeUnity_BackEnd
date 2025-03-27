using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;
using System.Reflection;

namespace CollegeUnity.API.Filters
{
    public class AddPaginationToResponseActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is not JsonResult jsonResult || jsonResult == null)
            {
                goto skip;
            }

            try
            {
                var Response = context.HttpContext.Response;
                var obj = jsonResult.Value;

                if (obj == null || obj.GetType().Name != typeof(ApiResponse<>).Name)
                {
                    goto skip;
                }

                Type objType = obj.GetType();

                PropertyInfo? dataProperty = objType.GetProperty(nameof(ApiResponse<object>.Data));

                // paged list object
                if (dataProperty == null || dataProperty.PropertyType.Name != typeof(PagedList<>).Name)
                {
                    goto skip;
                }

                object pagedListObj = dataProperty.GetValue(obj);
                if (pagedListObj == null)
                {
                    goto skip;
                }

                Type pagedListObjType = pagedListObj.GetType();

                MethodInfo? method = typeof(ApiResponseHelpers)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .FirstOrDefault(
                        m => m.Name == nameof(ApiResponseHelpers.AddPagination) &&
                        m.IsGenericMethod);

                if (method == null || !method.IsGenericMethod)
                {
                    goto skip;
                }

                method = method.MakeGenericMethod(pagedListObjType.GenericTypeArguments);

                method.Invoke(Response, [Response, pagedListObj]);
            }
            catch (Exception){ }

            skip:;
        }
    }
}
