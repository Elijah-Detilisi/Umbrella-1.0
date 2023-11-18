using System.Globalization;

namespace Application.Abstractions.Services.Audio;

public interface IMauiSpeechRecognition
{
    ValueTask DisposeAsync();
    Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken);
}
