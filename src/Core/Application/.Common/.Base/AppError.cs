namespace Application.Common.Base;

public class AppError
{
    //Properties
    public string Value { get; set; }
    public string Description { get; set; }

    //Construction
    public AppError(string value, string description)
    {
        Value = value;
        Description = description;
    }

    //Standard
    public static readonly AppError Crash = new("Crash", "Application crashed!");
    public static readonly AppError Error = new("Error", "An error has occured.");
    public static readonly AppError Cancelled = new("Cancelled", "The operation was cancelled."); 
    
}
