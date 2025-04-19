using EmailService.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Buffers.Text;
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
        private async Task<ForgetPasswordResultDto> _ForgetPassword(string name, string emailAddress, IConfiguration configuration)
        {
            try
            {
                var receiver = new ForgetPasswordDto(emailAddress, name);

                string subject = receiver.Subject;
                string body = EmailHelper.GetResetPasswordHtmlBody(receiver.ResetCode, receiver.RecieverName);
                var client = new SendGridClient(configuration["SenderGridService:ApiKey"]);
                var from = new EmailAddress(configuration["SenderGridService:SenderEmail"], configuration["SenderGridService:SenderName"]);
                var msg = new SendGridMessage()
                {
                    From = from,
                    Subject = subject,
                    HtmlContent = body,
                    PlainTextContent = $"Hi {receiver.RecieverName}, your reset code is: {receiver.ResetCode}"
                };
                var to = new EmailAddress(receiver.RecieverEmail, receiver.RecieverName);

                msg.AddTo(to);

                var response = await client.SendEmailAsync(msg);

                bool result = response.StatusCode == System.Net.HttpStatusCode.Accepted;

                if (result) {
                    // add the token to the result
                    string token = "";
                    return ForgetPasswordResultDto.Success("Reset Code Sent, Please Check Your Email", emailAddress, receiver.ResetCode);
                }
                else
                {
                    return ForgetPasswordResultDto.Fail("Something went Wrong!, Please try again later.");
                }
            }
            catch (Exception ex)
            {

                return ForgetPasswordResultDto.Fail($"Email sending failed: {ex.Message}");
            }
        }
    }
}
