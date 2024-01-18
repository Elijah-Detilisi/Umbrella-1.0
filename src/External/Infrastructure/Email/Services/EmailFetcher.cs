using Application.Email.Models;
using Application.Email.Services;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher
{
    Task<List<EmailModel>> IEmailFetcher.GetEmailsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
