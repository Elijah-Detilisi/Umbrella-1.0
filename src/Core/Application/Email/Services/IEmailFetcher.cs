using Application.Email.Models;

namespace Application.Email.Services;

public interface IEmailFetcher
{
    Task<List<EmailModel>> GetEmailsAsync(CancellationToken cancellationToken);
}
