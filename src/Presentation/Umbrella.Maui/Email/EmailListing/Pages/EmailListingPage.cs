using Umbrella.Maui.Email.Base.Pages;

namespace Umbrella.Maui.Email.EmailListing.Pages;

public class EmailListingPage : EmailPage<EmailListingViewModel>
{
    public EmailListingPage(EmailListingViewModel viewModel) : base("Email", viewModel)
    {
    }

    protected override ScrollView ContentView => new ScrollView()
    {
        BackgroundColor = Color.FromRgb(0, 12, 1)
    };
}
