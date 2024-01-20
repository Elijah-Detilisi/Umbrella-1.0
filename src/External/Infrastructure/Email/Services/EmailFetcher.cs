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
        var settings = GetPop3ServerSettings(userModel.EmailAddress.GetEmailDomain());

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
    private EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var processedText = ShortenUrls(mimeMessage.TextBody);
        var senderAddress = mimeMessage.Sender?.Address ?? "no-reply@email.com";
        
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Recipients = [_currentUser.EmailAddress],
            Sender = EmailAddress.Create(senderAddress),
            Body = EmailBodyText.Create(processedText),
            Subject = EmailSubjectLine.Create(mimeMessage.Subject),
        };

        return messageModel;
    }
    private static string ShortenUrls(string input)
    {
        // Define a regular expression pattern to match URLs
        string urlPattern = @"(https?://\S+)";

        // Use regex to find all matches
        MatchCollection matches = Regex.Matches(input, urlPattern);

        // Iterate through matches and shorten URLs
        foreach (Match match in matches.Cast<Match>())
        {
            string originalUrl = match.Groups[1].Value;
            string shortenedUrl = originalUrl.Length > 30 ? originalUrl.Substring(0, 30) + "..." : originalUrl;
            input = input.Replace(originalUrl, shortenedUrl);
        }

        return input;
    }

    //Disposal
    public void Dispose()
    {
        _pop3Client.Disconnect(true);
        _pop3Client.Dispose();
    }
}
