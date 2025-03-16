using CollegeUnity.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace CollegeUnity.API.Filters
{
    public class EditResponseActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
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
    }
}
