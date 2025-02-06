using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class Result
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string ResetCode { get; set; }

        public static Result Success(string message, string resetCode)
        {
            return new Result { Message = message, IsSuccess = true, ResetCode = resetCode };
        }
        
        public static Result Fail(string message)
        {
            return new Result { Message = message, IsSuccess = false };
        }
    }
}
