using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;

namespace Umbrella.Maui.Email.EmailDetail.Pages;

public class EmailDetailPage : EmailPage<EmailListingViewModel>
{
    public EmailDetailPage(EmailListingViewModel viewModel, ChatHistoryView chatHistoryView)
        : base("EmailDetail", viewModel, chatHistoryView)
    {
        InitializeShell();
    }

    private void InitializeShell()
    {
        Shell.SetBackgroundColor(this, Colors.White);
    }

    protected override ScrollView PageContent => new()
    {
        Content = new VerticalStackLayout()
        {
            //NavBar
            //Subject line
            //Sender details and control
            //Email body text


        }
    };
}
