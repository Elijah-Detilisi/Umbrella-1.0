namespace Infrastructure.Email.Services;

public class EmailFetcher : IEmailFetcher
{
    private readonly int _portNumber;
    private readonly string _serverName;
    private readonly UserModel _userModel;

    public EmailFetcher(UserModel userModel, string serverName, int portNumer)
    {
        _portNumber = portNumer;
        _serverName = serverName;
        _userModel = userModel;
    }

    public async Task<List<EmailModel>> GetEmailsAsync()
    {
        var allMessages = new List<EmailModel>();

        using (var client = new Pop3Client())
        {
            await client.ConnectAsync(_serverName, _portNumber, true);
            await client.AuthenticateAsync(
                _userModel.EmailAddress.ToString(),
                _userModel.EmailPassword.ToString()
            );

            for (int i = 0; i < client.Count; i++)
            {
                var mimeMessage = client.GetMessage(i);
                allMessages.Add(ConvertToEmailModel(mimeMessage));
            }

            await client.DisconnectAsync(true);
        }

        return allMessages;
    }

    private EmailModel ConvertToEmailModel(MimeMessage mimeMessage)
    {
        var messageModel = new EmailModel()
        {
            Type = EmailType.Email,
            EmailStatus = EmailStatus.UnRead,
            Subject = EmailSubjectLine.Create(mimeMessage.Subject),
            Body = EmailBodyText.Create(mimeMessage.Body.ToString()),
            Sender = EmailAddress.Create(mimeMessage.From.FirstOrDefault().Name),
            Recipients = new List<EmailAddress>() {
            EmailAddress.Create(mimeMessage.To.FirstOrDefault().Name)
        }
        };

        return messageModel;
    }
}
