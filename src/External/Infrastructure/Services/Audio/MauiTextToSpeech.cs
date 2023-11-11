namespace Infrastructure.Services.Audio;

public class MauiTextToSpeech
{
    public async Task Speak(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            await TextToSpeech.Default.SpeakAsync(text);
        }
    }
}
