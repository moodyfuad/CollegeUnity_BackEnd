using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class ForgetPasswordResultDto
    {
        private ForgetPasswordResultDto(string message, bool isSuccess, string? email = null,string? resetCode = null)
        {
            this.Message = message;
            this.IsSuccess = isSuccess;
            this.Email = email;
            this.ResetCode = resetCode;
        }
        #region Feilds

        public string? Token { get; set; }

        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public string? Email { get; set; }

        private string? ResetCode { get; set; }
        #endregion

       public static ForgetPasswordResultDto Success(string message, string email, string resetCode)
        {
            return new ForgetPasswordResultDto(message, true, email, resetCode);
        }

        public static ForgetPasswordResultDto Fail(string message)
        {
            return new ForgetPasswordResultDto(message, false);
        }
        
        public void SetToken(string token)
        {
            this.Token = token;
        }

        public string? GetResetCode()
        {
            return ResetCode??null;
        }

    }
}
