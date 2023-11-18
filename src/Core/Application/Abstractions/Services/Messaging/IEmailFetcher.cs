using Application.Models.Email;

namespace Infrastructure.Services.Messaging;

public interface IEmailFetcher
{
    Task<List<EmailModel>> GetEmailsAsync();
}