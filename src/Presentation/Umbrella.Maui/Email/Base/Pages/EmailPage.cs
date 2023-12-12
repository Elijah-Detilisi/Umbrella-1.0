using Umbrella.Maui.Email.Base.Views;

namespace Umbrella.Maui.Email.Base.Pages;

public abstract class EmailPage<TViewModel> : BasePage<TViewModel> where TViewModel : ViewModel
{
    //Fields
    private enum Row { Content = 0, ChatBox = 1 }

    //View components
    private Grid MainGridLayout { get; }
    public ChatHistoryView ChatHistory { get; set; }
    protected abstract ScrollView PageContent { get; }

    //Construction
    protected EmailPage(string title, TViewModel viewModel) : base(viewModel)
    {
        Title = title;

        MainGridLayout = new Grid();
        ChatHistory = new ChatHistoryView();

        InitializeEmailPage();
    }

    //Initialization
    protected virtual void InitializeEmailPage()
    {
        InitializeMainGridLayout();

        Padding = 0;
        Content = MainGridLayout;
    }

    //View component initialization
    private void InitializeMainGridLayout()
    {
        MainGridLayout.RowDefinitions = new RowDefinitionCollection
        {
            new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) },
            new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
        };

        MainGridLayout.Add(PageContent.Row(Row.Content));
        MainGridLayout.Add(ChatHistory.Row(Row.ChatBox));
    }
}