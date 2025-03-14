using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace WorkoutPlanner.Services
{
    public class EmailService(IConfiguration configuration)
    {
        public async Task SendEmailConfirmationAsync(string email, string confirmationLink)
        {
            var client = new SendGridClient(configuration["EmailService:SendGridApiKey"]);
            var from = new EmailAddress(configuration["EmailService:SenderEmail"], "Workout Planner");
            var subject = "Email Confirmation";
            var to = new EmailAddress(email);

            var htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1'>
                    <title>Email Confirmation</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .email-container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background: #ffffff;
                            padding: 30px;
                            border-radius: 8px;
                            box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
                            text-align: center;
                        }}
                        .header {{
                            font-size: 26px;
                            font-weight: bold;
                            color: #333333;
                            margin-bottom: 15px;
                        }}
                        .content {{
                            font-size: 16px;
                            color: #444444;
                            margin-top: 10px;
                            line-height: 1.6;
                        }}
                        .highlight {{
                            font-weight: bold;
                            color: #000000;
                        }}
                        .confirm-button {{
                            display: inline-block;
                            background-color: #000000;
                            color: white !important;
                            text-decoration: none;
                            font-size: 16px;
                            padding: 14px 28px;
                            border-radius: 6px;
                            margin-top: 25px;
                            font-weight: bold;
                        }}
                        .confirm-button:hover {{
                            background-color: #333333;
                        }}
                        .footer {{
                            font-size: 13px;
                            color: #777777;
                            margin-top: 25px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='header'>Welcome to Workout Planner!</div>
                        <div class='content'>
                            <p>You're receiving this message because you recently signed up for a <span class='highlight'>Workout Planner</span> account.</p>
                            <p>To get started, confirm your email address by clicking the button below:</p>
                        </div>
                        <a href='{confirmationLink}' class='confirm-button'>Confirm Email</a>
                        <div class='footer'>
                            If you didn’t sign up, you can safely ignore this email.
                        </div>
                    </div>
                </body>
                </html>";

            var plainTextContent = "Click the following link to confirm your email: " + confirmationLink;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            await client.SendEmailAsync(msg);
        }

        public async Task SendRestorePasswordAsync(string email, string resetLink)
        {
            var client = new SendGridClient(configuration["EmailService:SendGridApiKey"]);
            var from = new EmailAddress(configuration["EmailService:SenderEmail"], "Workout Planner");
            var subject = "Password Reset";
            var to = new EmailAddress(email);

            var htmlContent = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1'>
                    <title>Password Reset</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .email-container {{
                            max-width: 600px;
                            margin: 20px auto;
                            background: #ffffff;
                            padding: 30px;
                            border-radius: 8px;
                            box-shadow: 0px 2px 10px rgba(0, 0, 0, 0.1);
                            text-align: center;
                        }}
                        .header {{
                            font-size: 26px;
                            font-weight: bold;
                            color: #333333;
                            margin-bottom: 15px;
                        }}
                        .content {{
                            font-size: 16px;
                            color: #444444;
                            margin-top: 10px;
                            line-height: 1.6;
                        }}
                        .highlight {{
                            font-weight: bold;
                            color: #000000;
                        }}
                        .confirm-button {{
                            display: inline-block;
                            background-color: #000000;
                            color: white !important;
                            text-decoration: none;
                            font-size: 16px;
                            padding: 14px 28px;
                            border-radius: 6px;
                            margin-top: 25px;
                            font-weight: bold;
                        }}
                        .confirm-button:hover {{
                            background-color: #333333;
                        }}
                        .footer {{
                            font-size: 13px;
                            color: #777777;
                            margin-top: 25px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='email-container'>
                        <div class='header'>Welcome to Workout Planner!</div>
                        <div class='content'>
                            <p>We've received a request to reset the password for the <span class='highlight'>Workout Planner</span> account associated with {email}. No changes have been made to your account yet.</p>
                            <p>You can reset your password by clicking the button below:</p>
                        </div>
                        <a href='{resetLink}' class='confirm-button'>Reset Password</a>
                        <div class='footer'>
                            If you didn’t request a password reset, please contact support.
                        </div>
                    </div>
                </body>
                </html>";

            var plainTextContent = "Click the following link to go to the reset password page: " + resetLink;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            await client.SendEmailAsync(msg);
        }
    }
}
