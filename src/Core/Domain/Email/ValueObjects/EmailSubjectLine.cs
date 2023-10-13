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
            throw new ArgumentException("Subject line cannot be empty or null.");
        }

        if (subject.Length > MaxSubjectLineLength)
        {
            throw new ArgumentException($"Subject line exceeds the maximum length of {MaxSubjectLineLength} characters.");
        }

        if (subject.Contains('\n') || subject.Contains('\r'))
        {
            throw new ArgumentException("Subject line cannot contain newlines or carriage returns.");
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
