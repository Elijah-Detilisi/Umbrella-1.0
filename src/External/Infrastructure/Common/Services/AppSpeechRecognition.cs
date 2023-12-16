namespace Infrastructure.Common.Services;

public class AppSpeechRecognition : IAppSpeechRecognition
{
    //Fields

    public Task<string> ListenAsync(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RequestPermissions(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task StopListenAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }
}
