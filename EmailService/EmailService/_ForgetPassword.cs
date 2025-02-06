using EmailService.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.EmailService
{
    public partial class EmailServices
    {

        
        private async Task<Result> _ForgetPassword(string name, string emailAddress, IConfiguration configuration)
        {
            try
            {
                
                var receiver = new ForgetPasswordDto(emailAddress, name);

                string subject = receiver.Subject;
                string body = receiver.htmlBody;
                var client = new SendGridClient(configuration["SenderGridService:ApiKey"]);
                var from = new EmailAddress(configuration["SenderGridService:SenderEmail"], configuration["SenderGridService:SenderName"]);
                var toEmail = new EmailAddress(receiver.RecieverEmail,receiver.RecieverName);
                var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, receiver.ResetCode, body);
                var response = await client.SendEmailAsync(msg);

                bool result = response.StatusCode == System.Net.HttpStatusCode.Accepted;

                if (result) {
                    // add the token to the result
                    string token = "";
                    return Result.Success("Reset Code Sent, Please Check Your Email", emailAddress, receiver.ResetCode);
                }
                else
                {
                    return Result.Fail("Something went Wrong!, Please try again later.");
                }
            }
            catch (Exception ex)
            {

                return Result.Fail($"Email sending failed: {ex.Message}");
            }
        }
    }
}
