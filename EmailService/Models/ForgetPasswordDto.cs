using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class ForgetPasswordDto
    {
        public ForgetPasswordDto(string recieverEmail, string recieverName)
        {
            RecieverEmail = recieverEmail;
            RecieverName = recieverName;
            ResetCode = GenerateResetCode(6);
        }


        public string RecieverEmail { get; private set; }

        public string RecieverName { get; private set; }

        public string Subject { get; private set; } = "Reset Password Request";

        public string ResetCode { get; private set; }

        private string GenerateResetCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
