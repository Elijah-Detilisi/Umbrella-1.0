using MailKit;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private readonly UserModel _userModel;
    private readonly Pop3Client _pop3Client;

    //Construction
    public EmailFetcher(UserModel userModel)
    {
        _userModel = userModel;
        _pop3Client = new Pop3Client();
    }

    //Properties
    public bool IsConnected => 
        _pop3Client.IsConnected && _pop3Client.IsAuthenticated;

    //Methods
    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected) return;

        var pop3ServerSettings = GetPop3ServerSettings(_userModel.EmailAddress.GetEmailDomain());

        //Connect to server
        await _pop3Client.ConnectAsync
            (
                pop3ServerSettings.Server,
                pop3ServerSettings.Port,
                pop3ServerSettings.UseSsl, cancellationToken
            );

        //Authenticate user
        await _pop3Client.AuthenticateAsync
        (
            _userModel.EmailAddress.Value,
            _userModel.EmailPassword.Value, cancellationToken
        );
    }
    public List<EmailModel> GetEmailsAsync(CancellationToken cancellationToken = default)
    {
        //Verify connection
        if (!IsConnected)
        {
            throw new ServiceNotConnectedException();
        }

        //Retrieve messages
        var allMessages = new List<EmailModel>();

        for (int i = 0; i < _pop3Client.Count; i++)
        {
            var mimeMessage = _pop3Client.GetMessage(i);
            allMessages.Add(ConvertToEmailModel(mimeMessage));
        }

        return allMessages;
    }

    //Helper methods
    private static EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var senderAddress = mimeMessage.From.Select(x => x.Name).SingleOrDefault();
        var recipientAddresse = mimeMessage.To.Select(x => x.Name).SingleOrDefault();
        
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Subject = EmailSubjectLine.Create(mimeMessage.Subject),
            Body = EmailBodyText.Create(mimeMessage.Body.ToString()),
            Sender = EmailAddress.Create(senderAddress),
            Recipients = [ EmailAddress.Create(recipientAddresse) ]
        };

        return messageModel;
    }

    private static Pop3ServerSettings GetPop3ServerSettings(string emailDomain)
    {
        return emailDomain.ToLower() switch
        {
            "gmail.com" => Pop3ServerSettings.Gmail,
            "yahoo.com" => Pop3ServerSettings.Yahoo,
            "outlook.com" or "office365.com" => Pop3ServerSettings.Outlook,
            _ => throw new NotSupportedException($"Email provider for domain '{emailDomain}' is not supported."),
        };
    }

    //Disposal
    public void Dispose()
    {
        _pop3Client.Disconnect(true);
        _pop3Client.Dispose();
    }
}
