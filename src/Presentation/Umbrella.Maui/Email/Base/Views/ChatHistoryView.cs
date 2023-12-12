namespace Umbrella.Maui.Email.Base.Views;

public class ChatHistoryView : ContentView
{
    //View components
    private readonly Grid MainGridLayout;
    private readonly Frame MainFrameContainer;
    private readonly ImageButton ActionButton;
    private readonly ScrollView ChatHistoryContainer;
    private readonly CollectionView ChatHistoryCollection;

    //Conctruction
    public ChatHistoryView()
    {
        MainGridLayout = new Grid();
        MainFrameContainer = new Frame();
        ActionButton = new ImageButton();
        ChatHistoryContainer = new ScrollView();
        ChatHistoryCollection = new CollectionView();

        InitializeChatHistoryView();
    }

    //Initialization
    protected virtual void InitializeChatHistoryView()
    {
        InitializeActionButton();
        InitializeChatHistoryContainer();
        InitializeChatHistoryCollection();

        InitializeMainGridLayout();
        InitializeMainFrameContainer();

        Content = MainFrameContainer;
    }

    //Controls initialization
    private void InitializeActionButton()
    {
        ActionButton.WidthRequest = 40;
        ActionButton.HeightRequest = 40;
        ActionButton.Source = "umbrella_solid.svg";

        ActionButton.Row(1); // NB: Must be in bottom row
    }

    private void InitializeChatHistoryCollection()
    {
        ChatHistoryCollection.ItemTemplate(new DataTemplate());
        ChatHistoryCollection.SelectionMode = SelectionMode.None;
    }

    private void InitializeChatHistoryContainer()
    {
        ChatHistoryContainer.Content = ChatHistoryCollection;
        ChatHistoryContainer.Row(0); // NB: Must be in top row
    }

    private void InitializeMainGridLayout()
    {
        MainGridLayout.RowDefinitions = new RowDefinitionCollection()
        {
            new RowDefinition { Height = new GridLength(0.7, GridUnitType.Star) },
            new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
        };

        MainGridLayout.Add(ActionButton);
        MainGridLayout.Add(ChatHistoryContainer);
    }

    private void InitializeMainFrameContainer()
    {
        MainFrameContainer.Content = MainGridLayout;
        MainFrameContainer.DynamicResource(View.StyleProperty, "FrameMainChatContainer");
    }
}
