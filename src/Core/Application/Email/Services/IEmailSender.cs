using Application.Email.Models;

namespace Application.Email.Services;

public interface IEmailSender
{
    Task SendEmailAsync(EmailModel message);
}
