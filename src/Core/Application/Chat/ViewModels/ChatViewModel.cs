using Application.Chat.Models;
using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Application.Chat.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
        //Fieilds
        private bool microphoneUsable = false;

        //Services
        private readonly IAppTextToSpeech _textToSpeech;
        private readonly IAppSpeechRecognition _speechRecognition;

        //Properties
        public string CurrentPrompt { get; set; } = string.Empty; // Msg from VM to User
        public string CurrentCommand { get; set; } = string.Empty; // Msg from User to VM

        //Collections
        public ObservableCollection<ChatMessageModel> ChatMessageList { get; set; }

        //Construction
        public ChatViewModel
        (
            IAppTextToSpeech textToSpeech,
            IAppSpeechRecognition speechRecognition
        )
        {
            _textToSpeech = textToSpeech;
            _speechRecognition = speechRecognition;
            ChatMessageList = new ObservableCollection<ChatMessageModel>();
        }

        //MVVM Properties
        [ObservableProperty]
        public bool isListening = false;

        //Commands
        [RelayCommand]
        public async Task AuthorizeMicrophoneUsage(CancellationToken cancellationToken = default)
        {
            microphoneUsable = await _speechRecognition.RequestPermissions(cancellationToken);
        }

        [RelayCommand]
        public async Task StartListening(CancellationToken cancellationToken = default)
        {
            if (IsListening) return;

            try
            {
                IsListening = true;
                
                //Get permission
                if (!microphoneUsable)
                {
                    await _textToSpeech.SpeakAsync("Permission not granted.", cancellationToken); //Todo: Create application expection
                    return;
                }

                //Initiate Listen
                await _speechRecognition.ListenAsync(new Progress<string>(OnSpeechRecognized), cancellationToken);

            }
            catch
            {
                await _textToSpeech.SpeakAsync("An error has occured.", cancellationToken); //Todo: Create application expection
                throw;
            }
            finally 
            { 
                IsListening = false; 
            }
        }

        [RelayCommand]
        public async Task StopListening(CancellationToken cancellationToken = default)
        {
            if (!IsListening) return;
            
            IsListening = false;
            await _speechRecognition.StopListenAsync(cancellationToken);
        }

        //Handler methods
        private void OnSpeechRecognized(string partailText)
        {
            //Demo implemantion
            CurrentCommand = partailText;
            ChatMessageList.Add(new ChatMessageModel()
            {
                Sender = Enums.ChatSender.Human,
                Message = CurrentCommand
            });
        }
    }
}
