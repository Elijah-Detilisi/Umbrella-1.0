using Domain.User.ValueObjects;

namespace Application.User.Models;

public class UserModel : Model
{
    public EmailAddress EmailAddress { get; set; }
    public EmailPassword EmailPassword { get; set; }
    public string UserName { get; set; } = string.Empty;
}