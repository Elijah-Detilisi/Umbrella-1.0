namespace Domain.User;

public class UserAggregateRoot : AggregateRoot<EmailAddress>
{
    public EmailAddress EmailAddress { get; private set; }
    public EmailPassword EmailPassword { get; private set; }
    public string UserName { get; private set; } = string.Empty;

    private UserAggregateRoot(EmailAddress id, EmailPassword password) : base(id)
    {
        EmailAddress = id;
        EmailPassword = password;
    }

    public static UserAggregateRoot Create(EmailAddress emailAddress, EmailPassword password)
    {
        var user = new UserAggregateRoot(emailAddress, password);

        return user;
    }

    public void ChangeUserName(string newUsername)
    {
        UserName = newUsername;
    }

}
