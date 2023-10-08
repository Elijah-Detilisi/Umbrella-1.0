namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject
{
    private string Value { get; }
    private const int _minimumLength = 8;

    private EmailPassword(string value)
    {
        Value = value;
    }

    public static EmailPassword Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty or null.");
        }

        if (password.Length < _minimumLength)
        {
            throw new ArgumentException("Password must be at least 8 characters long.");
        }

        return new EmailPassword(password);
    }

    public override string ToString()
    {
        //return "********"; // Return a masked version of the password for security.
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
