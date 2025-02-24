using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Authentication.ForgetPasswordFeatures
{
    public class ForgetPasswordFeatureResultDto
    {

        public bool IsSuccess { get; }
        public string Message { get; }
        public string? Token { get; }
     
        public static ForgetPasswordFeatureResultDto Sent(string token, string message = null)
        {
            message ??= "Code Sent To Your Email";
            return new(true, message, token);
        }
        
        public static ForgetPasswordFeatureResultDto Reset(string message = null)
        {
            message ??= "Password Reset Successfully";
            return new(true, message);
        }
        
        public static ForgetPasswordFeatureResultDto Failed(string message = null)
        {
            message ??= "Failed Sending Reset Code";
            return new(false, message);
        }
        private ForgetPasswordFeatureResultDto(bool isSuccess, string message, string token = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Token = token;
        }
    }
}
