using Domain.Email.Entities.Enums;
using Domain.Email.ValueObjects;

namespace Application.Models.Email;

public class EmailModel
{
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    public EmailBodyText Body { get; set; }
    public EmailSubjectLine Subject { get; set; }
    public EmailAddress Sender { get; set; }
    public List<EmailAddress> Recipients { get;  set; }
}
