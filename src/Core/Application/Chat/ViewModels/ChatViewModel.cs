using Application.Chat.Models;
using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Application.Chat.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
        //Service
        private readonly IAppSpeechRecognition _speechRecognition;

        //Construction
        public ChatViewModel(IAppSpeechRecognition speechRecognition)
        {
            _speechRecognition = speechRecognition;
            ChatMessageList = new ObservableCollection<ChatMessageModel>();
        }

        //Properties
        [ObservableProperty]
        public string recognitionText = string.Empty;

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(ListenCommand))]
        public bool canListenExecute = true;

        [ObservableProperty, NotifyCanExecuteChangedFor(nameof(StopListenCommand))]
        bool canStopListenExecute = false;

        //Collections
        public ObservableCollection<ChatMessageModel> ChatMessageList { get; set; }

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

                await _speechRecognition.ListenAsync(
                    new Progress<string>(partialText =>
                    {
                        if (RecognitionText == beginSpeakingPrompt)
                        {
                            RecognitionText = string.Empty;
                        }

                        RecognitionText += partialText + " ";
                        ChatMessageList.Add(new ChatMessageModel()
                        {
                            Sender = Enums.ChatSender.Human,
                            Message = RecognitionText
                        });

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
        public async Task StopListen(CancellationToken cancellationToken)
        {
            CanListenExecute = true;
            CanStopListenExecute = false;

            await _speechRecognition.StopListenAsync(cancellationToken);
        }
    }
}
