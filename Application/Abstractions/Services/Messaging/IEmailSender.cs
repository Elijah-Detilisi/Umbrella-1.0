using Application.Models.Email;

namespace Application.Abstractions.Services.Messaging;

public interface IEmailSender
{
    Task SendEmailAsync(EmailModel message);
}
