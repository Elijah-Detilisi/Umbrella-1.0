using Application.Chat.Models;
using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Application.Chat.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
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
        public bool isListening = true;

        //Commands
        [RelayCommand]
        public async Task StartListening(CancellationToken cancellationToken)
        {
            if (IsListening) return;

            try
            {
                IsListening = true;
                
                //Get permission
                var isGranted = await _speechRecognition.RequestPermissions(cancellationToken);
                if (!isGranted)
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
        }

        [RelayCommand]
        public async Task StopListening(CancellationToken cancellationToken)
        {
            if (!IsListening) return;
            
            IsListening = false;
            await _speechRecognition.StopListenAsync(cancellationToken);
        }

        //Handler methods
        private void OnSpeechRecognized(string partailText)
        {
            throw new NotImplementedException();
        }
    }
}
