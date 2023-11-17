namespace Infrastructure.Services.Messaging;

public class EmailSender : IEmailSender
{
    private readonly int _portNumber;
    private readonly string _serverName;
    private readonly UserModel _userModel;

    public EmailSender(UserModel userModel, string serverName, int portNumer)
    {
        _portNumber = portNumer;
        _serverName = serverName;
        _userModel = userModel;
    }

    public async Task SendEmailAsync(EmailModel message)
    {
        using var client = new SmtpClient();
        var mimeMessage = ConvertToMimeMessage(message);
        
        await client.ConnectAsync(_serverName, _portNumber, true);
        await client.AuthenticateAsync(
            _userModel.EmailAddress.ToString(),
            _userModel.EmailPassword.ToString()
        );

        await client.SendAsync(mimeMessage);
        await client.DisconnectAsync(true);
    }

    private MimeMessage ConvertToMimeMessage(EmailModel message)
    {
        var builder = new BodyBuilder()
        {
            HtmlBody = message.Body.ToString()
        };
        var mimeMessage = new MimeMessage()
        {
            Subject = message.Subject.ToString(),
            Body = builder.ToMessageBody()
        };

        mimeMessage.From.Add(
            new MailboxAddress(_userModel.UserName, _userModel.EmailAddress.ToString())
        );

        foreach(var recipient in message.Recipients)
        {
            mimeMessage.To.Add(new MailboxAddress(recipient.ToString(), recipient.ToString()));
        }
        

        return mimeMessage;
    }
}
