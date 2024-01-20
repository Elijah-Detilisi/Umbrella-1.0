namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private readonly Pop3Client _pop3Client;

    //Construction
    public EmailFetcher()
    {
        _pop3Client = new();
    }

    //Properties
    public bool IsConnected => 
        _pop3Client.IsConnected && _pop3Client.IsAuthenticated;

    public List<EmailModel> AllEmails { 
        get 
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
    }

    //Methods
    public async Task ConnectAsync(UserModel userModel, CancellationToken cancellationToken = default)
    {
        if (IsConnected) return;

        var settings = GetPop3ServerSettings(userModel.EmailAddress.GetEmailDomain());

        //Connect to server
        _pop3Client.CheckCertificateRevocation = false;
        await _pop3Client.ConnectAsync
        (
            settings.Server,
            settings.Port,
            settings.UseSsl,
            cancellationToken
        );

        //Authenticate user
        await _pop3Client.AuthenticateAsync
        (
            userModel.EmailAddress.Value,
            userModel.EmailPassword.Value, cancellationToken
        );
    }

    //Helper methods
    private static EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var senderAddress = mimeMessage.From.Select(x => x.Name).FirstOrDefault();
        var recipientAddresse = mimeMessage.To.Select(x => x.Name).FirstOrDefault();
        
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Subject = EmailSubjectLine.Create(mimeMessage.Subject),
            Body = EmailBodyText.Create(mimeMessage.TextBody),
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
