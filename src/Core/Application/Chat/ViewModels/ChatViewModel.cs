using Application.Chat.Models;
using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Application.Chat.ViewModels
{
    /// <summary>
    /// Responsible for:
    /// - providing a verbal-user-interface (VUI)
    /// - record/log user-system interaction.
    /// Provides: 
    /// - method to get user verbal-input.
    /// - method to verbally-anounce system prompt.
    /// </summary>
    public partial class ChatViewModel : ViewModel
    {
        //Fieilds
        private bool microphoneUsable = false;

        //Services
        private readonly IAppTextToSpeech _textToSpeech;
        private readonly IAppSpeechRecognition _speechRecognition;

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

        //Methods
        public async Task AuthorizeMicrophoneUsage(CancellationToken cancellationToken = default)
        {
            microphoneUsable = await _speechRecognition.RequestPermissions(cancellationToken);
        }
        public async Task StopListenAsync(CancellationToken cancellationToken = default)
        {
            if (!IsListening) return;

            IsListening = false;
            await _speechRecognition.StopListenAsync(cancellationToken);
        }

        public async Task SpeakAsync(string messageText, CancellationToken cancellationToken = default)
        {
            if (IsListening || string.IsNullOrEmpty(messageText)) return;
            
            OnTextAnnouced(messageText);
            await _textToSpeech.SpeakAsync(messageText, cancellationToken);
        }

        public async Task ListenAsync(CancellationToken cancellationToken = default)
        {
            if (IsListening) return;

            try
            {
                IsListening = true;
                
                if (!microphoneUsable)
                {
                    await SpeakAsync("Permission not granted.", cancellationToken); //Todo: Create application expection
                    return;
                }

                //Initiate Listen
                var recognizedText = await _speechRecognition.ListenAsync(cancellationToken);
                if(string.IsNullOrWhiteSpace(recognizedText))
                {
                    OnSpeechRecognized(recognizedText);
                }

            }
            catch
            {
                await SpeakAsync("An error has occured.", cancellationToken); //Todo: Create application expection
                throw;
            }
            finally 
            { 
                IsListening = false; 
            }
        }
  
        //Handler methods
        private void OnSpeechRecognized(string text)
        {
            //Show user speech
            ChatMessageList.Add(new ChatMessageModel()
            {
                Sender = Enums.ChatSender.Human,
                Message = text
            });
        }
        private void OnTextAnnouced(string text)
        {
            //Show system prompt
            ChatMessageList.Add(new ChatMessageModel()
            {
                Sender = Enums.ChatSender.Bot,
                Message = text
            });
        }
    }
}
