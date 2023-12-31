using Application.Chat.ViewModels;

namespace Application.Email.Base;

public partial class EmailViewModel : ViewModel
{
    //Fields
    public ChatViewModel ChildViewModel { get; private set; }

    //Constructiom
    public EmailViewModel(ChatViewModel chatViewModel)
    {
        ChildViewModel = chatViewModel;
    }
}
