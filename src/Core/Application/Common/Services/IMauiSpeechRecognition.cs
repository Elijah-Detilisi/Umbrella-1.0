using System.Globalization;

namespace Application.Common.Services;

public interface IMauiSpeechRecognition
{
    ValueTask DisposeAsync();
    Task<bool> RequestPermissions(CancellationToken cancellationToken);
    Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken);
}