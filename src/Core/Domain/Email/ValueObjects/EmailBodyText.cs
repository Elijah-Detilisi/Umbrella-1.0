namespace Domain.Email.ValueObjects;

public class EmailBodyText : ValueObject<String>
{
    public const int MAXBODYLENGTH = 1000;
   
    private EmailBodyText(string value): base(value)
    {

    }

    public static EmailBodyText Create(string body)
    {
        if (string.IsNullOrWhiteSpace(body))
        {
            throw new EmptyValueException(nameof(EmailBodyText));
        }

        if (body.Length > MAXBODYLENGTH)
        {
            throw new EmailBodyTooLongException(MAXBODYLENGTH);
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