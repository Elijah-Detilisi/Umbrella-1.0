namespace Domain.Email.Exceptions;

public class EmailBodyTooLongException : Exception
{
    public EmailBodyTooLongException(int limitValue) : 
        base($"Email body content exceeds the maximum length of {limitValue} characters.")
    {
    }
}
