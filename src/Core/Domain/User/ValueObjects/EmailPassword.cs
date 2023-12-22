using Domain.User.Exceptions;

namespace Domain.User.ValueObjects;

public class EmailPassword : ValueObject<String>
{
    public const int MINIMUMPASSWORDLENGTH = 8; // 8 ALPHA-NUMERIC CHARACTERS

    private EmailPassword(string value):base(value)
    {
        
    }

    public static EmailPassword Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new EmptyValueException(nameof(EmailAddress));
        }

        if (password.Length < MINIMUMPASSWORDLENGTH)
        {
            throw new PasswordTooShortException(MINIMUMPASSWORDLENGTH);
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
