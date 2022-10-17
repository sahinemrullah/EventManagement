using MailKit;
using Microsoft.Extensions.Options;

namespace EventManagement.Application.Features.Mail
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string message);
    }
}
