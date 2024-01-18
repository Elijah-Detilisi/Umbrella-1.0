using Application.Email.Models;
using Application.Email.Services;
using Application.User.Models;
using Domain.Common.ValueObjects;
using Domain.Email.Entities.Enums;
using Domain.Email.ValueObjects;
using Infrastructure.Email.Settings;
using MimeKit;

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

        using (var client = new Pop3Client())
        {
            var portNumber = 1;
            await client.ConnectAsync("serverName", portNumber, true, cancellationToken);
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

    public static Pop3ServerSettings GetPop3ServerSettings(string emailDomain)
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
