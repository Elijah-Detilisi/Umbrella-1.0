namespace Domain.Common.ValueObjects;

public class EmailBodyText : ValueObject
{
    private const int MaxBodyLength = 1000;
    private string Value { get; }

    private EmailBodyText(string value)
    {
        Value = value;
    }

    public static EmailBodyText Create(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            throw new ArgumentException("Email body content cannot be empty or null.");
        }

        if (body.Length > MaxBodyLength)
        {
            throw new ArgumentException($"Email body content exceeds the maximum length of {MaxBodyLength} characters.");
        }

        return new EmailBodyText(body);
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
