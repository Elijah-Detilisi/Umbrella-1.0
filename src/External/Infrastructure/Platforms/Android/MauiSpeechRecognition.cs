using Android.Content;
using Android.Speech;

namespace Infrastructure;

/* All the code in this file is only included on Android.*/
public class MauiSpeechRecognition : IMauiSpeechRecognition
{
    //Fields
    private SpeechRecognizer speechRecognizer;
    private AndroidSpeechRecognitionListener listener;

    //Initializations
    public async Task<bool> RequestPermissions(CancellationToken cancellationToken)
    {
        var status = await Permissions.RequestAsync<Permissions.Microphone>();
        var isAvailable = SpeechRecognizer.IsRecognitionAvailable(Android.App.Application.Context);
        return status == PermissionStatus.Granted && isAvailable;
    }
    private Intent CreateSpeechIntent(CultureInfo culture)
    {
        var intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
        intent.PutExtra(RecognizerIntent.ExtraLanguagePreference, Java.Util.Locale.Default);
        var javaLocale = Java.Util.Locale.ForLanguageTag(culture.Name);
        intent.PutExtra(RecognizerIntent.ExtraLanguage, javaLocale);
        intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
        intent.PutExtra(RecognizerIntent.ExtraCallingPackage, Android.App.Application.Context.PackageName);
        intent.PutExtra(RecognizerIntent.ExtraPartialResults, true);

        return intent;
    }

    //Listen Methods
    public async Task<string> Listen(CultureInfo culture, IProgress<string> recognitionResult, CancellationToken cancellationToken)
    {
        var taskResult = new TaskCompletionSource<string>();
        listener = new AndroidSpeechRecognitionListener
        {
            Error = ex => taskResult.TrySetException(new Exception("Failure in speech engine - " + ex)),
            PartialResults = sentence =>
            {
                recognitionResult?.Report(sentence);
            },
            Results = sentence => taskResult.TrySetResult(sentence)
        };

        speechRecognizer = SpeechRecognizer.CreateSpeechRecognizer(Android.App.Application.Context);

        if (speechRecognizer is null)
        {
            throw new ArgumentException("Speech recognizer is not available");
        }

        speechRecognizer.SetRecognitionListener(listener);
        speechRecognizer.StartListening(CreateSpeechIntent(culture));

        await using (cancellationToken.Register(() =>
        {
            StopRecording();
            taskResult.TrySetCanceled();
        }))
        {
            return await taskResult.Task;
        }
    }

    //Stop methods
    private void StopRecording()
    {
        speechRecognizer?.StopListening();
        speechRecognizer?.Destroy();
    }

    //Disposal
    public async ValueTask DisposeAsync()
    {
        StopRecording();
        listener?.Dispose();
        speechRecognizer?.Dispose();
    }
}