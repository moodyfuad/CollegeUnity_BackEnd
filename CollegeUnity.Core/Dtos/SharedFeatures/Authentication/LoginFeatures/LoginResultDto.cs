using CollegeUnity.Core.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.SharedFeatures.Authentication.LoginFeatures
{
    public class LoginResultDto
    {
        public string? Token { get; private set; }
        public string[]? ErrorMessages = [];
        [JsonIgnore]
        public bool IsSuccess { get; private set; }


        private LoginResultDto(bool isSuccess,string? token = null,string[] errors = null)
        {
            IsSuccess = isSuccess;
            Token = token;
            ErrorMessages = errors;
        }

        public static LoginResultDto Failed(params string[] errors)
        {
            return new(false,errors: errors);
        }
        public static LoginResultDto Success(string token)
        {
            return new(true, token);
        }
    }
}
