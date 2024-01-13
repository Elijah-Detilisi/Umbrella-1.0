using Application.Chat.Errors;
using Application.Chat.Models;
using Application.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace Application.Chat.ViewModels
{
    /// <summary>
    /// Responsible for:
    /// - facilitating and logging user-system verbal interaction.
    /// Provides: 
    /// - verbal-user-interface (VUI)
    /// - method to get user verbal-input. (ListenAsync)
    /// - method to verbally-anounce system prompt. (SpeakAsync)
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

        //Authorization methods
        public async Task AuthorizeMicrophoneUsageAsync(CancellationToken cancellationToken = default)
        {
            microphoneUsable = await _speechRecognition.RequestPermissions(cancellationToken);
        }

        //Text-to-speech methods
        public async Task SpeakAsync(string messageText, CancellationToken cancellationToken = default)
        {
            if (IsListening || string.IsNullOrEmpty(messageText)) return;
            
            OnTextAnnouced(messageText);
            await _textToSpeech.SpeakAsync(messageText, cancellationToken);
        }

        //Speech-to-text methods
        public async Task StopListenAsync(CancellationToken cancellationToken = default)
        {
            if (!IsListening) return;

            IsListening = false;
            await _speechRecognition.StopListenAsync(cancellationToken);
        }
        public async Task ListenAsync(CancellationToken cancellationToken = default)
        {
            if (IsListening) return;

            try
            {
                IsListening = true;
                if (!microphoneUsable)
                {
                    await SpeakAsync(ChatError.PermissionError.Description, cancellationToken);
                    return;
                }

                var recognizedText = await _speechRecognition.ListenAsync(cancellationToken);
                if(string.IsNullOrWhiteSpace(recognizedText))
                {
                    OnSpeechRecognized(recognizedText);
                }
            }
            catch
            {
                await SpeakAsync(ChatError.RecognitionFailedError.Description, cancellationToken);
                throw;
            }
            finally 
            { 
                IsListening = false; 
            }
        }
  
        //Helper methods
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
