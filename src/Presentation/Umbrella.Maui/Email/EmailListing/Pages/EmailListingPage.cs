using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailListing.Views;

namespace Umbrella.Maui.Email.EmailListing.Pages;

public class EmailListingPage : EmailPage<EmailListingViewModel>
{
    public EmailListingPage(EmailListingViewModel viewModel, ChatHistoryView chatHistoryView) : base("Email", viewModel, chatHistoryView)
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
