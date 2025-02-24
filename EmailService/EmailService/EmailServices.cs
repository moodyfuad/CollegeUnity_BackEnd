using EmailService.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EmailService.EmailService
{
    public partial class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;

        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ForgetPasswordResultDto> ForgetPassword(string name, string emailAddress)
        {
            return await _ForgetPassword(name,emailAddress,_configuration);
        }
    }
}
