using Application.Email.Models;

namespace Application.Email.Services;

public interface IEmailFetcher : IDisposable
{
    bool IsConnected { get; }
    Task ConnectAsync(CancellationToken cancellationToken);
    List<EmailModel> GetAllEmails();
}
