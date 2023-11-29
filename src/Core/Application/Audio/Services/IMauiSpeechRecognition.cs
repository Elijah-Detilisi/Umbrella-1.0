using System.Globalization;

namespace Application.Audio.Services;

public interface IMauiSpeechRecognition
{
    ValueTask DisposeAsync();
    Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken);
}