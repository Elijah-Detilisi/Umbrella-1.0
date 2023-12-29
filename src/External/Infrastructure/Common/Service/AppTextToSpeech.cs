namespace Infrastructure.Common.Service;

public class AppTextToSpeech : IAppTextToSpeech
{
    //Fields
    private readonly ITextToSpeech textToSpeech;

    //Properties
    public float Pitch { get; private set; }
    public float Volume { get; private set; }
    
    //Construction
    public AppTextToSpeech()
    {
        textToSpeech = TextToSpeech.Default;
    }

    //Options method
    public void SetSpeechOptions(float pitch, float volume)
    {
        Pitch = pitch;
        Volume = volume;
    }

    //Speak method
    public async Task SpeakAsync(string text, CancellationToken cancelToken = default)
    {
        var options = new SpeechOptions()
        {
            Pitch = this.Pitch,
            Volume = this.Volume,
        };

        await textToSpeech.SpeakAsync(text, options, cancelToken);
    }
}
