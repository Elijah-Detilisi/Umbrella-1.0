namespace Domain.Email.Entities;

public class EmailEntity : Entity
{
    public EmailType Type { get; set; }
    public EmailStatus EmailStatus { get; set; }
    public EmailBodyText Body { get; private set; }
    public EmailSubjectLine Subject { get; private set; }
    public List<EmailAddress> Recipients { get; private set; }

    private EmailEntity(
        int id, List<EmailAddress> recipients, EmailSubjectLine subject, EmailBodyText body) : base(id)
    {
        Body = body;
        Subject = subject;
        Recipients = recipients;
        Type = EmailType.Email;
        EmailStatus = EmailStatus.UnRead;
    }

    public static EmailEntity Create(
        List<EmailAddress> recipients, EmailSubjectLine subject, EmailBodyText body)
    {
        var newEmailEntity = new EmailEntity(0, recipients, subject, body);

        return newEmailEntity;
    }
}
