namespace Domain.Common.ValueObjects;

public class EmailServerSetting : ValueObject<Tuple<int, string>>
{
    public EmailServerSetting(Tuple<int, string> value) : base(value)
    {
    }

    public static EmailServerSetting Create(int portNumber, string serverName)
    {
        if (string.IsNullOrWhiteSpace(serverName))
        {
            throw new EmptyValueException(nameof(serverName));
        }

        if (portNumber <= 0)
        {
            throw new ArgumentException("Port number must be greater than zero.", nameof(portNumber));
        }

        return new EmailServerSetting(Tuple.Create(portNumber, serverName));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
