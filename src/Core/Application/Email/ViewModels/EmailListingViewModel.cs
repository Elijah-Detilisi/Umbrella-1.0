using Application.Chat.ViewModels;
using Application.Email.Base;
using Application.Email.Models;
using Application.Email.Services;
using Application.User.Models;
using System.Collections.ObjectModel;

namespace Application.Email.ViewModels;

public class EmailListingViewModel : EmailViewModel
{
    //Services
    private readonly IEmailFetcher _emailFetcher;

    //Collection
    public ObservableCollection<EmailModel> EmailList { get; set; }

    //Construction
    public EmailListingViewModel
    (
        IEmailFetcher emailFetcher,
        ChatViewModel chatViewModel
    ) 
    : base(chatViewModel)
    {
        EmailList = new();
        _emailFetcher = emailFetcher;
    }

    //ViewModel life-cycle
    public override async void OnViewModelStarting
    (
        CancellationToken cancellationToken = default
    )
    {
        base.OnViewModelStarting(cancellationToken);

        await LoadEmailsAsync(cancellationToken);
    }

    //Load methods
    private async Task LoadEmailsAsync
    (
        CancellationToken cancellationToken = default
    )
    {
        //Establish connection
        await _emailFetcher.ConnectAsync(new UserModel(),cancellationToken);

        //Retrieve email message
        var allEmails = _emailFetcher.AllEmails;
        if (allEmails is null) return;

        EmailList = new(allEmails);
    }

    //Commands

    //Keep list of emails in inbox.
    //Populate list with lastest emails.
    //Commands to view an email.
    //Command to delete an email.
    //Command to right an email
    //Command to search for an email
    //Command to get latest email
    //Command to read through all emails
}
