using Application.Audio.Services;

namespace Infrastructure.Audio.Services;
public class MauiTextToSpeech : IMauiTextToSpeech
{
    public async Task SpeakAsync(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            await TextToSpeech.Default.SpeakAsync(text);
        }
    }
}