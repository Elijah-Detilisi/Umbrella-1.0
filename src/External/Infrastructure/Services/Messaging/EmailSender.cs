using Domain.User.Entities;

namespace Infrastructure.Services.Messaging;

public class EmailSender : IEmailSender
{
    private readonly int _portNumber;
    private readonly string _serverName;
    private readonly UserEntity _userEntity;

    public EmailSender(UserEntity userEntity, string serverName, int portNumer)
    {
        _portNumber = portNumer;
        _serverName = serverName;
        _userEntity = userEntity;
    }

    public async Task SendEmailAsync(MimeMessage message)
    {
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_serverName, _portNumber, true);
            await client.AuthenticateAsync(
                _userEntity.EmailAddress.ToString(), 
                _userEntity.EmailPassword.ToString()
            );

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

}
