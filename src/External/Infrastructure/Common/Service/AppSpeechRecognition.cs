namespace Infrastructure.Common.Service;

public class AppSpeechRecognition : IAppSpeechRecognition
{
    //Fields
    private readonly ISpeechToText _speechToText;
    private const string _defaultLanguage = "en-US";

    //Construction
    public AppSpeechRecognition()
    {
        _speechToText = SpeechToText.Default;
    }

    //Permision method
    public Task<bool> RequestPermissions(CancellationToken cancellationToken = default)
    {
        return _speechToText.RequestPermissions(cancellationToken);
    }

    //Listen method
    public async Task ListenAsync(IProgress<string>? recognitionResult, CancellationToken cancellationToken = default)
    {
        await _speechToText.ListenAsync(CultureInfo.GetCultureInfo(_defaultLanguage), recognitionResult, cancellationToken);
    }

    //Stop method
    public Task StopListenAsync(CancellationToken cancellationToken = default)
    {
        return _speechToText.StopListenAsync(cancellationToken);
    }

    //Dispose method
    public ValueTask DisposeAsync()
    {
        return _speechToText.DisposeAsync();
    }
}
