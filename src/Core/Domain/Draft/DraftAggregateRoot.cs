namespace Domain.Draft;

public class DraftAggregateRoot : AggregateRoot<Guid>
{
    public Guid Id { get; set; }
    public List<EmailAddress> Recipients { get; private set; }
    public EmailSubjectLine Subject { get; private set; }
    public EmailBodyText Body { get; private set; }


    private DraftAggregateRoot(Guid id) : base(id)
    {
        Id = id;
    }

    public static DraftAggregateRoot Create(List<EmailAddress> recipients, EmailSubjectLine subject, EmailBodyText body)
    {
        var draft = new DraftAggregateRoot(new Guid())
        {
            Body = body,
            Subject = subject,
            Recipients = recipients
        };

        return draft;
    }
}
