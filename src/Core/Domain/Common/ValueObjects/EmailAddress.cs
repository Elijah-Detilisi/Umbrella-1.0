namespace Domain.Common.ValueObjects;

public class EmailAddress : ValueObject
{
    private string Value { get; }

    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Create(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new ArgumentException("Email address cannot be empty or null.");
        }

        if (!IsValidEmail(emailAddress))
        {
            throw new ArgumentException("Invalid email address format.");
        }

        return new EmailAddress(emailAddress);
    }

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private static bool IsValidEmail(string email)
    {
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, emailPattern);
    }
}