namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject
{
    private string Value { get; }
    private const int _minimumPasswordLength = 8;

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

        if (password.Length < _minimumPasswordLength)
        {
            throw new ArgumentException(
                $"Password must be at least {_minimumPasswordLength} characters long."
            );
        }

        return new EmailPassword(password);
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
