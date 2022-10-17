using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace EventManagement.Application.Features.Mail
{
    internal class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public EmailService(IConfiguration configuration)
        {
            _mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
        }
        public async Task SendEmailAsync(string to, string message)
        {
            MimeMessage email = new()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Mail)
            };

            email.To.Add(MailboxAddress.Parse(to));

            email.Subject = "Your event declined by Admin.";

            BodyBuilder builder = new()
            {
                HtmlBody = message
            };

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);

            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
