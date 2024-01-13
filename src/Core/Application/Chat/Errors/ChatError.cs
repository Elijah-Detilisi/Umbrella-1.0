namespace Application.Chat.Errors;

public class ChatError : AppError
{
    public ChatError(string value, string description) : base(value, description)
    {
    }

    public static readonly AppError PermissionError = new("PermissionError", "Permission not granted to use device microphone.");
    public static readonly AppError RecognitionFailedError = new("SpeechRecognitionFailed", "Speech recognition has failed!");
}
