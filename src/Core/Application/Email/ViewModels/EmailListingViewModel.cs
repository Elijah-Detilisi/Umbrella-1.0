using Application.Chat.ViewModels;
using Application.Email.Base;

namespace Application.Email.ViewModels;

public class EmailListingViewModel : EmailViewModel
{
    public EmailListingViewModel(ChatViewModel chatViewModel) : base(chatViewModel)
    {
    }

    //Keep list of emails in inbox.
    //Populate list with lastest emails.
    //Commands to view an email.
    //Command to delete an email.
    //Command to right an email
    //Command to search for an email
    //Command to get latest email
    //Command to read through all emails
}
