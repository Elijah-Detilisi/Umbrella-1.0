using Application.Abstractions.Services.Audio;

namespace Umbrella.App
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private IMauiTextToSpeech _mauiTextToSpeech;

        public MainPage(IMauiTextToSpeech mauiTextToSpeech)
        {
            _mauiTextToSpeech = mauiTextToSpeech;
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            _mauiTextToSpeech.SpeakAsync("Killing me softly; " + count).GetAwaiter();
            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}