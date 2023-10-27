namespace Domain.Common.ValueObjects;

public class EmailAddress : ValueObject<string>
{
    private EmailAddress(string value) : base(value)
    {
        
    }

    public static EmailAddress Create(string emailAddress)
    {
        if (string.IsNullOrWhiteSpace(emailAddress))
        {
            throw new EmptyValueException(nameof(EmailAddress));
        }

        if (!IsValidEmail(emailAddress))
        {
            throw new InvalidEmailAddressException();
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