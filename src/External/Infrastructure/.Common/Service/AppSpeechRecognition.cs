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
    public async Task<string> ListenAsync(CancellationToken cancellationToken = default)
    {
        var recognitionResult = await _speechToText.ListenAsync(
            CultureInfo.GetCultureInfo(_defaultLanguage), new Progress<string>(), cancellationToken
        );

        if (recognitionResult.IsSuccessful)
        {
            return recognitionResult.Text;
        }

        return "[INFO]: Failed to recognized";
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
