using Application.Chat.ViewModels;
using Application.Email.Base;
using Application.Email.Models;

namespace Application.Email.ViewModels;

public class EmailDetailViewModel : EmailViewModel
{
    //Properties
    public EmailModel? Email { get; set; }

    //Construction
    public EmailDetailViewModel(ChatViewModel chatViewModel) : base(chatViewModel)
    {
    }

    //ViewModel lifecycle
    public override void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        base.OnViewModelStarting(cancellationToken);

        Email = new EmailModel()
        {
            SenderName = "Brutus Planner",
            Sender = EmailAddress.Create("brutus@gmail.com"),
            Subject = EmailSubjectLine.Create("Subject: Catching Up Soon"),
            Body = EmailBodyText.Create(
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In facilisis nulla eu felis fringilla vulputate.\n" +
                "Nullam porta eleifend lacinia. Donec at iaculis tellus. " + "Nullam porta eleifend lacinia. Donec at iaculis tellus."
                + "Nullam porta eleifend lacinia. Donec at iaculis tellus. " + "Nullam porta eleifend lacinia. Donec at iaculis tellus."
            ),
            CreatedAt = DateTime.Now,
        };
    }
}
