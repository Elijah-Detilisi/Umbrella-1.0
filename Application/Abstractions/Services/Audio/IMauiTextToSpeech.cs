namespace Application.Abstractions.Services.Audio;

public interface IMauiTextToSpeech
{
    Task SpeakAsync(string text);
}
