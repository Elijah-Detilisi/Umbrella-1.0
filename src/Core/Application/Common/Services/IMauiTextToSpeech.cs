namespace Application.Common.Services;

public interface IMauiTextToSpeech
{
    Task SpeakAsync(string text);
}

