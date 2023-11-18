using Application.Models.Email;

namespace Application.Abstractions.Services.Messaging;

public interface IEmailFetcher
{
    Task<List<EmailModel>> GetEmailsAsync();
}
