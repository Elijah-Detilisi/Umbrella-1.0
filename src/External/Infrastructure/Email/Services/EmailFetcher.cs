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

        _currentUser = userModel;
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
            userModel.EmailPassword.Value, 
            cancellationToken
        );
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
        var senderAddress = mimeMessage.Sender.Address;
        var processedText = ShortenUrls(mimeMessage.TextBody);

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
