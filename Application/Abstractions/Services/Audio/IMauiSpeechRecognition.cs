using System.Globalization;

namespace Infrastructure
{
    public interface IMauiSpeechRecognition
    {
        ValueTask DisposeAsync();
        Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken);
    }
}