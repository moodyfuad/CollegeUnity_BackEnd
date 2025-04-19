using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    internal static class EmailHelper
    {
        public const string logo_filename = "college_logo.png";
        public const string logo_mime_type = "image/png";
        public const string logo_disposition = "inline";
        public const string logo_content_id = "logo_cid";
        public const string logo_alt_text = "College Logo";

        public static string GetLogoImageHtml()
        {
            string src = $@"<img src = 'https://lh3.googleusercontent.com/pw/AP1GczNJ7GRXiDKDnpzqkC8BhPsz28lp7mM21aeMqi14xJ_D2tjjm94I7v6gscKKiuqt6RQE1N6IgM6D-j0qWo_qkBpnhSDVu1BdqN0IKknVUYMbIfQvC5TR_h0iq-xYrONIdP7koUhiQWhruOmsAD0xyhZ6=w1308-h869-s-no-gm?authuser=0' alt = '{logo_alt_text}' class='logo'>";
            return src;
        }

        public static string GetLogoImageAsBase64()
        {
            string basePath = AppContext.BaseDirectory;
            string logoPath = Path.Combine(basePath, "college_logo.png");
            byte[] imageBytes = File.ReadAllBytes(logoPath);
            string base64Image = Convert.ToBase64String(imageBytes, Base64FormattingOptions.None);

            return base64Image;
        }

        public static string GetResetPasswordHtmlBody(string ResetCode, string recieverName)
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
                            width: 250px;
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
                        {GetLogoImageHtml()}
                        <div class='header'>Password Reset Request</div>
                        <p class='message'>
                            Hello, {recieverName.ToUpper()}.
                        </p>
                        <p class='message'>
                            You have requested to reset your password. Please use the following code to proceed with resetting your password:
                        </p>
                        <div class='code'>{ResetCode}</div>
                        <p class='message'>If you did not request this, you can ignore this email.</p>
                        
                        <p class='footer'>
                            This is an automated email, please do not reply. If you need help, contact our support  team.
                        </p>
                    </div>

                </body>
                </html>";

            return html;
        }
    }

}
