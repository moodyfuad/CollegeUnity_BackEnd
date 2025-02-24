using EmailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public interface IEmailServices
    {
        Task<ForgetPasswordResultDto> ForgetPassword(string name,string emailAddress);
    }
}
