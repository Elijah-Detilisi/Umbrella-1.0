namespace Domain.Email.ValueObjects;

public class EmailSubjectLine : ValueObject<string>
{
    public const int MAXSUBJECTLINELENGTH = 150; 

    private EmailSubjectLine(string value) : base(value)
    {

    }

    public static EmailSubjectLine Create(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new EmptyValueException(nameof(EmailSubjectLine));
        }

        if (subject.Length > MAXSUBJECTLINELENGTH)
        {
            throw new SubjectLineTooLongException(MAXSUBJECTLINELENGTH);
        }

        if (subject.Contains('\n') || subject.Contains('\r'))
        {
            throw new InvalidSubjectException();
        }

        return new EmailSubjectLine(subject);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
