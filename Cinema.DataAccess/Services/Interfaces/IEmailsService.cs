namespace Cinema.DataAccess.Services.Interfaces
{
    public interface IEmailsService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
