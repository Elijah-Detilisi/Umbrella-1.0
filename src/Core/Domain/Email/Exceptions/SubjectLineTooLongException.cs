namespace Domain.Email.Exceptions;

public class SubjectLineTooLongException : Exception
{
    public SubjectLineTooLongException(int limitValue) : 
        base($"Subject line exceeds the maximum length of {limitValue} characters.")
    {
    }
}
