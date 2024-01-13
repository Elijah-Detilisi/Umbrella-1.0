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

    //ViewModel lifecylce
    public override async void OnViewModelStarting(CancellationToken cancellationToken = default)
    {
        base.OnViewModelStarting(cancellationToken);

        await ChildViewModel.AuthorizeMicrophoneUsageAsync(cancellationToken);
    }

    public override void OnViewModelClosing(CancellationToken cancellationToken = default)
    {
        base.OnViewModelClosing(cancellationToken);

        ChildViewModel.OnViewModelClosing(cancellationToken);
    }
}
