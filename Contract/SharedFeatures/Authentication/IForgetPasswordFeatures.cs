using CollegeUnity.Core.Dtos.SharedFeatures.Authentication.ForgetPasswordFeatures;
using CollegeUnity.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Authentication
{
    public interface IForgetPasswordFeatures
    {

        // => token {email, ForgetPasswordStatus.CodeSent , expires=5m}
        Task<ForgetPasswordFeatureResultDto> SendResetPasswordCode(string email);

        // => token {email ,ForgetPasswordStatus.Reset , expires=5m}
        Task<ForgetPasswordFeatureResultDto> ValidateVerificationCode(string email, string code);

        Task<ForgetPasswordFeatureResultDto> ResetPassword(string email, string newPassword);
        // params (email, newPass) => (bool, Token)

        
    }
}
