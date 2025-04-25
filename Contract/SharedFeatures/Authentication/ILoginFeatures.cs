using CollegeUnity.Core.Dtos.AuthenticationDtos;
using CollegeUnity.Core.Dtos.ResponseDto;
using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Authentication
{
    public interface ILoginFeatures
    {
        Task<LoginResultDto> Login(
             UserLoginDto userLoginDto,
             DateTime? expireAt = null);

        Task<ApiResponse<string?>> AcceptWaitingStudent(string cardId);
    }
}
