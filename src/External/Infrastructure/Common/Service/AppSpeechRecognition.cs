namespace Infrastructure.Common.Service;

public class AppSpeechRecognition : IAppSpeechRecognition
{
    //Fields
    private readonly ISpeechToText speechToText;
    private const string defaultLanguage = "en-US";

    //Construction
    public AppSpeechRecognition()
    {
        speechToText = SpeechToText.Default;
    }

    //Permision method
    public Task<bool> RequestPermissions(CancellationToken cancellationToken = default)
    {
        return speechToText.RequestPermissions(cancellationToken);
    }

    //Listen method
    public async Task ListenAsync(IProgress<string>? recognitionResult, CancellationToken cancellationToken = default)
    {
        await speechToText.ListenAsync(CultureInfo.GetCultureInfo(defaultLanguage), recognitionResult, cancellationToken);
    }

    //Stop method
    public Task StopListenAsync(CancellationToken cancellationToken = default)
    {
        return speechToText.StopListenAsync(cancellationToken);
    }

    //Dispose method
    public ValueTask DisposeAsync()
    {
        return speechToText.DisposeAsync();
    }
}
