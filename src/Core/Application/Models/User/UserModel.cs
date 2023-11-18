using Domain.User.ValueObjects;

namespace Application.Models.User;

public class UserModel
{
    public EmailAddress EmailAddress { get; set; }
    public EmailPassword EmailPassword { get; set; }
    public string UserName { get; set; } = string.Empty;
}

