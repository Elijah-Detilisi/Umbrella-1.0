namespace Umbrella.Maui.Email.Base.Views;

public class ChatHistoryView : ContentView
{
    //ViewElements
    private Grid MainGridLayout;
    private Frame MainFrameContainer;
    private ImageButton ActionButton;
    private CollectionView ChatHistory;
    private ScrollView ChatHistoryContainer;
    
    
    //Conctruction
    public ChatHistoryView()
    {
        MainGridLayout = new Grid();
        MainFrameContainer = new Frame();
        ActionButton = new ImageButton();
        ChatHistory = new CollectionView();
        ChatHistoryContainer = new ScrollView();

        Content = MainFrameContainer;
    }

    //Initialization
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

    private void InitializeActionButton()
    {
        ActionButton.WidthRequest = 40;
        ActionButton.HeightRequest = 40;
        ActionButton.Source = "umbrella_solid.svg";

        ActionButton.Row(1); // NB: Must be in bottom row
    }

    private void InitializeChatHistoryContainer()
    {
        ChatHistoryContainer.Content = ChatHistory;
        ChatHistoryContainer.Row(0); // NB: Must be in top row
    }

}
