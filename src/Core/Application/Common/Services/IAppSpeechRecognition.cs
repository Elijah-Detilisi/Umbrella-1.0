using System.Globalization;

namespace Application.Common.Services;

public interface IAppSpeechRecognition : IAsyncDisposable
{
    Task<bool> RequestPermissions(CancellationToken cancellationToken = default);
    Task StopListenAsync(CancellationToken cancellationToken = default);
    Task ListenAsync(IProgress<string>? recognitionResult, CancellationToken cancellationToken = default);
}
