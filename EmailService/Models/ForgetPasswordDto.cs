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
            htmlBody = GetResetPasswordHtmlBody(ResetCode);
        }


        public string RecieverEmail { get; private set; }
        public string RecieverName { get; private set; }
        public string Subject { get; private set; } = "Reset Password Request";
        public string ResetCode { get; private set; }
        public string htmlBody { get; private set; }



        private string GenerateResetCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GetResetPasswordHtmlBody(string ResetCode)
        {
            string html = $@"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Password Reset</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }}
            .container {{
                max-width: 600px;
                background-color: #ffffff;
                margin: 50px auto;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
                text-align: center;
            }}
            .logo {{
                width: 120px;
                margin-bottom: 20px;
            }}
            .header {{
                font-size: 24px;
                font-weight: bold;
                color: #333;
                margin-bottom: 10px;
            }}
            .message {{
                font-size: 16px;
                color: #555;
                line-height: 1.5;
                margin-bottom: 20px;
            }}
            .code {{
                font-size: 22px;
                font-weight: bold;
                background-color: #f8f8f8;
                padding: 10px;
                display: inline-block;
                border-radius: 5px;
                letter-spacing: 3px;
                color: #333;
                margin-bottom: 20px;
            }}
            .button {{
                display: inline-block;
                background-color: #007BFF;
                color: white;
                padding: 12px 20px;
                border-radius: 5px;
                text-decoration: none;
                font-size: 16px;
                margin-top: 10px;
            }}
            .button:hover {{
                background-color: #0056b3;
            }}
            .footer {{
                font-size: 12px;
                color: #777;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>

        <div class='container'>
            <img src='https://hu.edu.ye/compfac/wp-content/uploads/2022/01/041e2fd9-8748-459c-bfc2-0bfafe75b29d.png' alt='Company Logo' class='logo'>
            <div class='header'>Password Reset Request</div>
            <p class='message'>
                You have requested to reset your password. Please use the following code to proceed with resetting your password:
            </p>
            <div class='code'>{ResetCode}</div>
            <p class='message'>If you did not request this, you can ignore this email.</p>
            
            <p class='footer'>
                This is an automated email, please do not reply. If you need help, contact our support team.
            </p>
        </div>

    </body>
    </html>";

            return html;
        }
    }

}
