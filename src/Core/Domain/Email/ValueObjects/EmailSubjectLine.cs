namespace Domain.Email.ValueObjects;

public class EmailSubjectLine : ValueObject
{
    private const int MaxSubjectLineLength = 100; // Define your maximum length requirement here.

    private string Value { get; }

    private EmailSubjectLine(string value)
    {
        Value = value;
    }

    public static EmailSubjectLine Create(string subject)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new EmptyValueException(nameof(EmailSubjectLine));
        }

        if (subject.Length > MaxSubjectLineLength)
        {
            throw new SubjectLineTooLongException(MaxSubjectLineLength);
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
