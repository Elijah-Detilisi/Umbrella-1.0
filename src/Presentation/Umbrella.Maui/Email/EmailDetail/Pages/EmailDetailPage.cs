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
        /*Shell.SetBackgroundColor(this, Colors.White);
        Shell.SetTitleView(this, new HorizontalStackLayout()
        {
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Fill,
            BackgroundColor = Colors.Yellow,
        });*/


        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "star_solid.png"
        });
        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "delete_solid.png"
        });
        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "share_solid.png"
        });
    }

    protected override ScrollView PageContent => new()
    {
        Content = new VerticalStackLayout()
        {
            //Subject line
            //Sender details and control
            //Email body text
        }
    };
}
