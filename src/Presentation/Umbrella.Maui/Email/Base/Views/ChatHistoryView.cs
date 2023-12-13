using Umbrella.Maui.Email.Base.Controls;

namespace Umbrella.Maui.Email.Base.Views;

public class ChatHistoryView : ContentView
{
    //Fields
    private enum Row { Top = 0, Bottom = 1 }

    //View components
    private readonly Grid ChatHistoryGrid;
    private readonly Frame ChatHistoryFrame;
    private readonly ImageButton ActionButton;
    private readonly ScrollView ChatHistoryScrollView;
    private readonly CollectionView ChatHistoryCollection;

    //Conctruction
    public ChatHistoryView()
    {
        ChatHistoryGrid = new Grid();
        ChatHistoryFrame = new Frame();
        ActionButton = new ImageButton();
        ChatHistoryScrollView = new ScrollView();
        ChatHistoryCollection = new CollectionView();

        InitializeView();
    }

    //Initialization
    protected virtual void InitializeView()
    {
        InitializeActionButton();
        InitializeChatCollectionView();
        InitializeChatScrollView();

        InitializeChatGrid();
        InitializeChatFrame();

        Content = ChatHistoryFrame;
    }

    //View component initialization
    private void InitializeActionButton()
    {
        ActionButton.WidthRequest = 40;
        ActionButton.HeightRequest = 40;
        ActionButton.Source = "umbrella_solid.svg";

        ActionButton.Row(Row.Bottom);
    }

    private void InitializeChatCollectionView()
    {
        ChatHistoryCollection.ItemsSource = new List<string>()
        {
            "Hello world",
            "Hello world",
            "Hello world",
        };

        ChatHistoryCollection.ItemTemplate(new ChatDataTemplate());
        ChatHistoryCollection.SelectionMode = SelectionMode.None;
    }

    private void InitializeChatScrollView()
    {
        ChatHistoryScrollView.Content = ChatHistoryCollection;
        ChatHistoryScrollView.Row(Row.Top);
    }

    private void InitializeChatGrid()
    {
        ChatHistoryGrid.RowDefinitions = new RowDefinitionCollection()
        {
            new RowDefinition { Height = new GridLength(0.7, GridUnitType.Star) },
            new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
        };

        ChatHistoryGrid.Add(ActionButton);
        ChatHistoryGrid.Add(ChatHistoryScrollView);
    }

    private void InitializeChatFrame()
    {
        ChatHistoryFrame.Content = ChatHistoryGrid;
        ChatHistoryFrame.DynamicResource(View.StyleProperty, "ChatHistoryFrame");
    }
}
