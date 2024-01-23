using Infrastructure.Email.Extensions;
using System.Text.RegularExpressions;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private UserModel _currentUser;
    private readonly Pop3Client _pop3Client;

    //Construction
    public EmailFetcher()
    {
        _pop3Client = new();
        _currentUser = new();

        //Set up pop3Client
        _pop3Client.CheckCertificateRevocation = false;
    }

    //Properties
    public bool IsConnected => 
        _pop3Client.IsConnected && _pop3Client.IsAuthenticated;

    //Methods
    public async Task ConnectAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        if (IsConnected) return;

        _currentUser = userModel;
        var settings = Pop3ServerSettings.FindPop3ServerSettings(userModel.EmailAddress.GetEmailDomain());

        //Connect to server
        await _pop3Client.ConnectAsync(settings.Server, settings.Port, settings.UseSsl, cancellationToken);

        //Authenticate user
        await _pop3Client.AuthenticateAsync(userModel.EmailAddress.Value, userModel.EmailPassword.Value, cancellationToken);
    }

    public async Task<List<EmailModel>> LoadEmailsAsync(CancellationToken cancellationToken = default)
    {
        //Verify connection
        if (!IsConnected)
        {
            throw new ServiceNotConnectedException();
        }

        //Retrieve messages
        var allMessages = new List<EmailModel>();

        for (int i = 0; i < _pop3Client.GetMessageCount(); i++)
        {
            var mimeMessage = await _pop3Client.GetMessageAsync(i);
            allMessages.Add(ConvertToEmailModel(mimeMessage));
        }

        return allMessages;
    }

    //Helper methods
    private EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var senderAddress = mimeMessage.From.Mailboxes.Select(x=>x.Address).FirstOrDefault();
        
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Recipients = [_currentUser.EmailAddress],
            Sender = EmailAddress.Create(senderAddress ?? "no-reply@email.com"),
            Subject = EmailSubjectLine.Create(mimeMessage.Subject.ShortText() ?? "No subject"),
            Body = EmailBodyText.Create(mimeMessage.TextBody.ShortText()?? "No message text."),
        };

        return messageModel;
    }
    
    //Disposal
    public void Dispose()
    {
        _pop3Client.Disconnect(true);
        _pop3Client.Dispose();
    }
}
