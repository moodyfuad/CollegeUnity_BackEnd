using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Contract.SharedFeatures.Authentication
{
    public interface IForgetPasswordFeatures
    {
        // ValidateCredentials
            // params (email) => bool

        // SendVerificationCodeViaEmail
            // params (email) => (isSuccess, Code, message, Token)
             // store the code in user's VerificationCode

        // ValidateVerificationCode
            // params (email, Code) => (bool, Token)

        // ResetPassword
            // params (email, newPassword)
    }
}
