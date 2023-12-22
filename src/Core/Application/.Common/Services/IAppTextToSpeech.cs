namespace Application.Common.Services;

public interface IAppTextToSpeech
{
    Task SetSpeechOptions(float pitch, float volume);
    Task SpeakAsync(string text, CancellationToken cancelToken = default);
}
