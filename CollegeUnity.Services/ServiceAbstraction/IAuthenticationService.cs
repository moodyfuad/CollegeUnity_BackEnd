using CollegeUnity.Core.Dtos.AuthenticationDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Services.ServiceAbstraction
{
    public interface IAuthenticationService
    {
        Task<string> Login(UserLoginDto loginDto, DateTime? expireAt = null);
        Task<string> SignUp(UserSignUpDto userSignUpDto);
        static abstract UserClaimsDto GetUserClaims(in HttpContext context);

    }
}
