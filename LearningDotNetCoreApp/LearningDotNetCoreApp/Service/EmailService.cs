using required.Modals;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace required.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        public async Task SendEmailAsync(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = ReplacePlaceHolder("This is email subject from book store application", emailOptions.PlaceHolder);
            emailOptions.Body =  ReplacePlaceHolder(await GetEmailBOdy("TestEmail"),emailOptions.PlaceHolder);
            await SendEmail(emailOptions);
        }

        public async Task SendEmailForEmailConfirmationAsync(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = ReplacePlaceHolder("Confirm your email id", emailOptions.PlaceHolder);
            emailOptions.Body = ReplacePlaceHolder(await GetEmailBOdy("EmailConfirm"), emailOptions.PlaceHolder);
            await SendEmail(emailOptions);
        }

        public async Task SendEmailForForgotpasswordAsync(UserEmailOptions emailOptions)
        {
            emailOptions.Subject = ReplacePlaceHolder("Hello {{UserName}} reset your book store web app password", emailOptions.PlaceHolder);
            emailOptions.Body = ReplacePlaceHolder(await GetEmailBOdy("ForgotPassword"), emailOptions.PlaceHolder);
            await SendEmail(emailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTMl
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);

        }

        private async Task<string> GetEmailBOdy(string templateName)
        {
            var body = await File.ReadAllTextAsync(string.Format(templatePath, templateName));
            return body;
        }


        private string ReplacePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null && keyValuePairs.Count > 0)
            {
                foreach (var keyValue in keyValuePairs)
                {
                    if(text.Contains(keyValue.Key))
                    {
                        text = text.Replace(keyValue.Key, keyValue.Value);
                    }

                }
            }
            return text;
        }
    }
}
