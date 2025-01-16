using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace CollegeUnity.API.Filters
{
    public class ModelValidateActionFilter : IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid) {context.Result = new NotFoundResult() ; }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status414UriTooLong;
            }
            
        }

    }
}
