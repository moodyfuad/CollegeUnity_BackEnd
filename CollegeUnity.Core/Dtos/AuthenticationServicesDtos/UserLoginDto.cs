using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CollegeUnity.Core.Dtos.AuthenticationDtos
{
    public abstract class UserLoginDto
    {
        public UserLoginDto() { }
        protected UserLoginDto(string Token)
        {
            this.Token = Token;
            IsSuccess = true;
        }
        protected UserLoginDto(params string[] errors)
        {
            this.ErrorMessages = errors;
            IsSuccess = false;
        }
       
        [Required(ErrorMessage = "Password is required field")]
        [MinLength(4)]
        public string? Password { get; set; }
        
        public string? Token { get; private set; }
        public string[] ErrorMessages = [];
        public bool IsSuccess { get; private set; }

    }
}
