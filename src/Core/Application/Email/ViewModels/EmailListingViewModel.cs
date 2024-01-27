using Application.Chat.ViewModels;
using Application.Email.Base;
using Application.Email.Models;
using Application.Email.Services;
using Application.User.Models;
using Domain.User.ValueObjects;
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
        var currentUser =  new UserModel()
        {
            EmailAddress = EmailAddress.Create("xxxxxxxx@xxxx.com"),
            EmailPassword = EmailPassword.Create("xxxxxxxx"),
        };

        await _emailFetcher.ConnectAsync(currentUser, cancellationToken);

        //Retrieve email message
        var allEmails = await _emailFetcher.LoadEmailsAsync(cancellationToken);
        if (allEmails is null) return;

        foreach (var item in allEmails)
        {
            EmailList.Add(item);
        }
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
