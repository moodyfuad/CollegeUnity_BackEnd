using CollegeUnity.Contract.EF_Contract;
using CollegeUnity.Core.Dtos.ResponseDto;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CollegeUnity.API.Middlerware_Extentions;

public class ActiveUserAttribute : Attribute, IAsyncActionFilter
{
    private readonly IRepositoryManager _repositoryManager;

    public ActiveUserAttribute(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        int? userId = context.HttpContext.User.GetUserId();

        if (userId is null || userId == 0)
        {
            await next();
            return;
        }

        var accountStatus = await _repositoryManager.UserRepository.GetAccountStatus(userId.Value);

        if (accountStatus == CollegeUnity.Core.Enums.AccountStatus.Deactive)
        {
            throw new UnauthorizedAccessException("User account is inactive");
        }

        await next();
    }
}
