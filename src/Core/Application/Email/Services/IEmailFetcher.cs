using Application.Email.Models;

namespace Application.Email.Services;

public interface IEmailFetcher
{
    List<EmailModel> GetEmailsAsync(CancellationToken cancellationToken);
}
