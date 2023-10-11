namespace Domain.User;

public class UserAggregateRoot : AggregateRoot<EmailAddress>
{
    public EmailAddress EmailAddress { get; private set; }
    public EmailPassword EmailPassword { get; private set; }
    public string UserName { get; private set; } = string.Empty;

    private UserAggregateRoot(
        EmailAddress id, 
        EmailPassword password, 
        string username
    ) : base(id)
    {
        EmailAddress = id;
        UserName = username;
        EmailPassword = password;
    }

    public static UserAggregateRoot Create(
        EmailAddress emailAddress, EmailPassword password,string username
    )
    {
        var user = new UserAggregateRoot(emailAddress, password, username);

        return user;
    }
}
