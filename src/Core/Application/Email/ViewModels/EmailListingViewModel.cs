using Application.Chat.ViewModels;
using Application.Email.Base;

namespace Application.Email.ViewModels;

public class EmailListingViewModel : EmailViewModel
{
    public EmailListingViewModel(ChatViewModel chatViewModel) : base(chatViewModel)
    {
    }
}
