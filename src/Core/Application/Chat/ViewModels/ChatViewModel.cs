using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.Globalization;

namespace Application.Chat.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
        //Service
        private readonly IMauiSpeechRecognition _speechRecognition;

        //Construction
        public ChatViewModel(IMauiSpeechRecognition speechRecognition)
        {
            _speechRecognition = speechRecognition;
        }

        //Properties
        [ObservableProperty]
        public string recognitionText = string.Empty;

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ListenCommand))]
        public bool canListenExecute = true;

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(StopListenCommand))]
        bool canStopListenExecute = false;

        //Commands
        [RelayCommand(IncludeCancelCommand = true, CanExecute = nameof(CanListenExecute))]
        public async Task Listen(CancellationToken cancellationToken)
        {
            try
            {
                CanStopListenExecute = true;
                var isGranted = await _speechRecognition.RequestPermissions(cancellationToken);
                if (!isGranted)
                {
                    Debug.WriteLine("Permission not granted");
                    return;
                }
                var beginSpeakingPrompt = "Begin speaking...";
                RecognitionText = beginSpeakingPrompt;

                RecognitionText = await _speechRecognition.Listen(CultureInfo.GetCultureInfo("en-us"),
                    new Progress<string>(partialText =>
                    {
                        if (RecognitionText == beginSpeakingPrompt)
                        {
                            RecognitionText = string.Empty;
                        }

                        RecognitionText += partialText + " ";
                    }), cancellationToken);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                Debug.WriteLine("You said: " + RecognitionText);
            }
        }

        [RelayCommand(CanExecute = nameof(CanStopListenExecute))]
        public void StopListen(CancellationToken cancellationToken)
        {
            CanListenExecute = true;
            CanStopListenExecute = false;
        }
    }
}
