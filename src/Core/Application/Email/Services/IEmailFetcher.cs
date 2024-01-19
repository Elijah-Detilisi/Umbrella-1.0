using Application.Email.Models;
using Application.User.Models;

namespace Application.Email.Services;

public interface IEmailFetcher : IDisposable
{
    //Properties
    bool IsConnected { get; }
    List<EmailModel> AllEmails { get; }

    //Methods
    Task ConnectAsync(UserModel userModel, CancellationToken cancellationToken);
}
