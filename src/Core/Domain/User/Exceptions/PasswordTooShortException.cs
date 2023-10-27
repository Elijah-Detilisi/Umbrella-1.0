namespace Domain.User.Exceptions;

public class PasswordTooShortException: Exception
{
    public PasswordTooShortException(int limitValue) :
        base($"Password must be at least {limitValue} characters long.")
    {
    }
}
