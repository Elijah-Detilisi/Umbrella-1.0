using Domain.User.Entities;

namespace Infrastructure.Services.Messaging;

public class EmailFetcher
{
    private readonly int _portNumber;
    private readonly string _serverName;
    private readonly UserEntity _userEntity;

    public EmailFetcher(UserEntity userEntity, string serverName, int portNumer)
    {
        _portNumber = portNumer;
        _serverName = serverName;
        _userEntity = userEntity;
    }

    public async Task<List<MimeMessage>> GetEmailsAsync()
    {
        var allMessages = new List<MimeMessage>();

        using (var client = new Pop3Client())
        {
            await client.ConnectAsync(_serverName, _portNumber, true);
            await client.AuthenticateAsync(_userEntity.EmailAddress.ToString(), _userEntity.EmailPassword.ToString());

            for (int i = 0; i < client.Count; i++)
            {
                var mimeMessage = client.GetMessage(i);
                allMessages.Add(mimeMessage);
            }

            await client.DisconnectAsync(true);
        }

        return allMessages;
    }
}
