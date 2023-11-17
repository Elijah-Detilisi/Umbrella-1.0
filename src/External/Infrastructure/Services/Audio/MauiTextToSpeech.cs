using Application.Abstractions.Services.Audio;

namespace Infrastructure.Services.Audio;

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
