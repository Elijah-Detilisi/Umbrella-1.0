using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailDetail.Pages;
using Umbrella.Maui.Email.EmailListing.Templates;

namespace Umbrella.Maui.Email.EmailListing.Pages;

public class EmailListingPage : EmailPage<EmailListingViewModel>
{
    public EmailListingPage(EmailListingViewModel viewModel, ChatHistoryView chatHistoryView) 
        : base("Email", viewModel, chatHistoryView)
    {
        
    }

    protected override ScrollView PageContent => new()
    {
        Content = new CollectionView
        {
            SelectionMode = SelectionMode.Single,
            ItemTemplate = new EmailDataTemplate(),
            ItemsSource = BindingContext.EmailList,

        }
        .Invoke(collectionView => collectionView.SelectionChanged += HandleSelectionChanged)
    };

    private async void HandleSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var current = e.CurrentSelection;
        await Shell.Current.GoToAsync(nameof(EmailDetailPage));
    }
}
