namespace Infrastructure.Services.Audio;

public class MauiTextToSpeech
{
    public static async Task SpeakAsync(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            await TextToSpeech.Default.SpeakAsync(text);
        }
    }
}
