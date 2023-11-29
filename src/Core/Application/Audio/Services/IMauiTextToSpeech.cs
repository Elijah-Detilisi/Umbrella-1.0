namespace Application.Audio.Services;

public interface IMauiTextToSpeech
{
    Task SpeakAsync(string text);
}

