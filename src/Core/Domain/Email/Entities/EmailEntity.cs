namespace Domain.Email.Entities;

public class EmailEntity : Entity
{
    public List<EmailAddress> Recipients { get; private set; }
    public EmailSubjectLine Subject { get; private set; }
    public EmailBodyText Body { get; private set; }

    private EmailEntity(
        int id, List<EmailAddress> recipients, EmailSubjectLine subject, EmailBodyText body) : base(id)
    {
        Body = body;
        Subject = subject;
        Recipients = recipients;
    }

    public static EmailEntity Create(
        List<EmailAddress> recipients, EmailSubjectLine subject, EmailBodyText body)
    {
        var newEmailEntity = new EmailEntity(0, recipients, subject, body);

        return newEmailEntity;
    }
}
