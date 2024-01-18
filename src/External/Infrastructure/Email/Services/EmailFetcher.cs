namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher
{
    //Fields
    private readonly UserModel _userModel;

    //Construction
    public EmailFetcher(UserModel userModel)
    {
        _userModel = userModel;
    }

    //Methods
    public async Task<List<EmailModel>> GetEmailsAsync(CancellationToken cancellationToken = default)
    {
        var allMessages = new List<EmailModel>();
        var pop3ServerSettings = GetPop3ServerSettings(_userModel.EmailAddress.GetEmailDomain());

        using (var client = new Pop3Client())
        {
            await client.ConnectAsync
            (
                pop3ServerSettings.Server, 
                pop3ServerSettings.Port, 
                pop3ServerSettings.UseSsl, cancellationToken
            );

            await client.AuthenticateAsync
            (
                _userModel.EmailAddress.Value, 
                _userModel.EmailPassword.Value, cancellationToken
            );

            for (int i = 0; i < client.Count; i++)
            {
                var mimeMessage = client.GetMessage(i);
                allMessages.Add(ConvertToEmailModel(mimeMessage));
            }
        }

        return allMessages;
    }

    //Helper methods

    private static EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Subject = EmailSubjectLine.Create(mimeMessage.Subject),
            Body = EmailBodyText.Create(mimeMessage.Body.ToString()),
            Sender = EmailAddress.Create(mimeMessage.From.FirstOrDefault().Name),
            Recipients = [
                EmailAddress.Create(mimeMessage.To.FirstOrDefault().Name)
            ]
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
}
