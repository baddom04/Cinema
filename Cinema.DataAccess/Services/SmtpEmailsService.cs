using Cinema.DataAccess.Config;
using Cinema.DataAccess.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Cinema.DataAccess.Services
{
    public class SmtpEmailsService : IEmailsService
    {
        private readonly EmailSettings _emailSettings;
        public SmtpEmailsService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using MailMessage mail = new MailMessage();

            mail.From = new MailAddress(_emailSettings.FromEmail);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            using SmtpClient smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port);
            smtp.Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password);
            smtp.EnableSsl = _emailSettings.EnableSsl;
            await smtp.SendMailAsync(mail);
        }
    }
}
