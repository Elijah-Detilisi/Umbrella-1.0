using Infrastructure.Email.Extensions;
using MailKit.Net.Imap;
using MailKit.Search;

namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher, IDisposable
{
    //Fields
    private UserModel _currentUser;
    private readonly ImapClient _imapClient;

    //Construction
    public EmailFetcher()
    {
        _imapClient = new();
        _currentUser = new();

        //Set up client
        _imapClient.CheckCertificateRevocation = false;
    }

    //Properties
    public bool IsConnected => 
        _imapClient.IsConnected && _imapClient.IsAuthenticated;

    //Methods
    public async Task ConnectAsync(UserModel userModel, CancellationToken token = default)
    {
        if (IsConnected) return;

        _currentUser = userModel;
        var settings = ImapServerSettings.FindServerSettings(userModel.EmailAddress.GetEmailDomain());

        //Connect to server
        await _imapClient.ConnectAsync(settings.Server, settings.Port, settings.UseSsl, token);

        //Authenticate user
        await _imapClient.AuthenticateAsync(userModel.EmailAddress.Value, userModel.EmailPassword.Value, token);
    }

    public async Task<List<EmailModel>> LoadEmailsAsync(CancellationToken token = default)
    {
        //Verify connection
        if (!IsConnected)
        {
            throw new ServiceNotConnectedException();
        }

        //Retrieve messages
        var allMessages = new List<EmailModel>();

        // Select the Inbox folder
        await _imapClient.Inbox.OpenAsync(FolderAccess.ReadOnly, token);

        // Search for all messages in the Inbox
        var uids = await _imapClient.Inbox.SearchAsync(SearchQuery.All, token);

        // Fetch the messages
        foreach (var uid in uids)
        {
            var mimeMessage = await _imapClient.Inbox.GetMessageAsync(uid, token);
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
            CreatedAt = mimeMessage.Date.DateTime,
            Sender = EmailAddress.Create(senderAddress ?? "no-reply@email.com"),
            Subject = EmailSubjectLine.Create(mimeMessage.Subject.ShortText() ?? "No subject"),
            Body = EmailBodyText.Create(mimeMessage.TextBody.ShortText()?? "No message text."),
        };

        return messageModel;
    }
    
    //Disposal
    public void Dispose()
    {
        _imapClient.Disconnect(true);
        _imapClient.Dispose();
    }
}
