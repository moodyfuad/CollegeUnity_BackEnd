using CollegeUnity.Core.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;

namespace CollegeUnity.Contract.Services_Contract.ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginDto loginDto, DateTime? expireAt = null);
        Task<string> SignUp(UserSignUpDto userSignUpDto);
        static abstract UserClaimsDto GetUserClaims(in HttpContext context);

    }
}
