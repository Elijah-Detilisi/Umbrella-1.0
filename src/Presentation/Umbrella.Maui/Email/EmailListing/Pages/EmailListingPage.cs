using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.EmailListing.Views;

namespace Umbrella.Maui.Email.EmailListing.Pages;

public class EmailListingPage : EmailPage<EmailListingViewModel>
{
    public EmailListingPage(EmailListingViewModel viewModel) : base("Email", viewModel)
    {
    }

    protected override ScrollView PageContent => new ScrollView()
    {
        Content = new VerticalStackLayout()
        {
            new EmailListItemView(),
            new EmailListItemView(),
            new EmailListItemView(),
            new EmailListItemView(),
        }
    };
}
