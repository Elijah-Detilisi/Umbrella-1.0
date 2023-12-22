namespace Domain.Common.Exceptions;

public class EmptyValueException : Exception
{
    public EmptyValueException(string valueName) : 
        base($"{valueName} cannot be empty or null.")
    {
    }
}
