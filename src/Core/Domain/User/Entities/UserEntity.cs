namespace Domain.User.Entities;

public class UserEntity : Entity
{
    public EmailAddress EmailAddress { get; private set; }
    public EmailPassword EmailPassword { get; private set; }
    public string UserName { get; private set; } = string.Empty;

    private UserEntity(
        int id, EmailAddress emailAddress, EmailPassword password, string username) : base(id)
    {
        UserName = username;
        EmailPassword = password;
        EmailAddress = emailAddress;
    }

    public static UserEntity Create(
        EmailAddress emailAddress, EmailPassword password, string username)
    {
        var newUserEntity = new UserEntity(0, emailAddress, password, username);

        return newUserEntity;
    }
}
