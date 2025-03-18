using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Expressions;
using System.Reflection;

namespace CollegeUnity.API.Filters
{
    public class EditResponseActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            AddStatusCodeToResponse(ref context);

            AddPaginationToResponse(ref context);
        }

        private static void AddStatusCodeToResponse(ref ActionExecutedContext context)
        {
            var result = context.Result;
            if (result is JsonResult jsonResult && jsonResult != null)
            {
                try
                {
                    var val = jsonResult.Value;
                    var valType = val.GetType();

                    PropertyInfo? statusCodeProperty = valType.GetProperties().
                        FirstOrDefault(p => p.Name == "StatusCode", null);

                    object statusCode =
                        statusCodeProperty == null || statusCodeProperty.GetValue(val) == null ?
                        jsonResult.StatusCode! :
                        statusCodeProperty.GetValue(val)!;

                    jsonResult.StatusCode = (int)statusCode;
                }
                catch (Exception) { }
            }
        }

        private static void AddPaginationToResponse(ref ActionExecutedContext context)
        {
            if (context.Result is JsonResult jsonResult && jsonResult != null)
            {
                try
                {
                    var obj = jsonResult.Value;

                    if (obj == null || obj.GetType().Name != typeof(ApiResponse<>).Name)
                    {
                        goto skip;
                    }

                    Type objType = obj.GetType();

                    // paged list info
                    PropertyInfo? dataProperty = objType.GetProperty("Data");

                    // paged list object
                    if (dataProperty == null || dataProperty.PropertyType.Name != typeof(PagedList<>).Name)
                    {
                        goto skip;
                    }

                    object pagedListObj = dataProperty.GetValue(obj)!;

                    Type dataType = pagedListObj!.GetType();

                    PropertyInfo[]? pagedListObjProperties = dataType.GetProperties();

                    int currentPage = (int)pagedListObjProperties.FirstOrDefault(
                        p => p.Name == "CurrentPage")?.GetValue(pagedListObj)!;

                    int totalPages = (int)pagedListObjProperties.FirstOrDefault(
                        p => p.Name == "TotalPages")?.GetValue(pagedListObj)!;

                    int pageSize = (int)pagedListObjProperties.FirstOrDefault(
                        p => p.Name == "PageSize")?.GetValue(pagedListObj)!;

                    bool hasPrevious = (bool)pagedListObjProperties.FirstOrDefault(
                        p => p.Name == "HasPrevious")?.GetValue(pagedListObj)!;

                    bool hasNext = (bool)pagedListObjProperties.FirstOrDefault(
                        p => p.Name == "HasNext")?.GetValue(pagedListObj)!;

                    context.HttpContext.Response.AddPagination(
                        pageNumber: currentPage,
                        totalPages: totalPages,
                        pageSize: pageSize,
                        hasPrevious: hasPrevious,
                        hasNext: hasNext);

                skip:;
                }
                catch (Exception) { }
            }
        }
    }
}
