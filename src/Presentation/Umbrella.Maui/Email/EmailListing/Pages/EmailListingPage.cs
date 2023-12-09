using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.EmailListing.Views;

namespace Umbrella.Maui.Email.EmailListing.Pages;

public class EmailListingPage : EmailPage<EmailListingViewModel>
{
    public EmailListingPage(EmailListingViewModel viewModel) : base("Email", viewModel)
    {
    }

    protected override ScrollView ContentView => new ScrollView()
    {
        //BackgroundColor = Color.FromRgb(0, 12, 1),
        Content = new VerticalStackLayout()
        {
            new EmailListItemView(),
            new EmailListItemView(),
            new EmailListItemView(),
            new EmailListItemView(),
        }
    };
}
