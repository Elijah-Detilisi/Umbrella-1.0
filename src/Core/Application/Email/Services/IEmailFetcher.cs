using Application.Email.Models;

namespace Application.Email.Services;

public interface IEmailFetcher : IDisposable
{
    bool IsConnected { get; }
    List<EmailModel> GetEmailsAsync(CancellationToken cancellationToken);
}
