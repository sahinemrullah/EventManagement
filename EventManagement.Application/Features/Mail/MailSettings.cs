namespace EventManagement.Application.Features.Mail
{
    public class MailSettings
    {
        public string Host { get; set; } = null!;
        public int Port { get; set; }
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}