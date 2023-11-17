using Domain.User.ValueObjects;

namespace Application.Models.User;

public class UserModel
{
    public EmailAddress EmailAddress { get; private set; }
    public EmailPassword EmailPassword { get; private set; }
    public string UserName { get; private set; } = string.Empty;
}
