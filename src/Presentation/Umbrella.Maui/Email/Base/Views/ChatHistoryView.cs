using System.Collections;
using Umbrella.Maui.Email.Base.Controls;

namespace Umbrella.Maui.Email.Base.Views;

public class ChatHistoryView : ContentView
{
    //Fields
    private enum Row { Top = 0, Bottom = 1 }

    //View components
    private Grid? ChatHistoryGrid;
    private Frame? ChatHistoryFrame;
    private ImageButton? ActionButton;
    private ScrollView? ChatHistoryScrollView;
    private CollectionView? ChatHistoryCollection;

    //Conctruction
    public ChatHistoryView()
    {
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
        ActionButton = new ImageButton
        {
            WidthRequest = 40,
            HeightRequest = 40,
            Source = "umbrella_solid.svg"
        };
        ActionButton.Row(Row.Bottom);
    }

    private void InitializeChatCollectionView()
    {
        ChatHistoryCollection = new CollectionView
        {
            SelectionMode = SelectionMode.None,
            ItemTemplate = new ChatDataTemplate(),
            ItemsSource = new List<ChatMessageModel>()
            {
                new(){  Sender = ChatSender.Bot, Message = "Hello, this is a chat message 1."},
                new(){  Sender = ChatSender.Human, Message = "Hello, this is a chat message 2."},
                new(){  Sender = ChatSender.Bot, Message = "Hello, this is a chat message 3."}
            }
        };
    }

    private void InitializeChatScrollView()
    {
        ChatHistoryScrollView = new ScrollView
        {
            Content = ChatHistoryCollection
        };

        ChatHistoryScrollView.Row(Row.Top);
    }

    private void InitializeChatGrid()
    {
        var topRowSize = 0.7;
        var bottomRowSize = 0.3;

        ChatHistoryGrid = new Grid
        {
            RowDefinitions = new RowDefinitionCollection()
            {
                new RowDefinition { Height = new GridLength(topRowSize, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(bottomRowSize, GridUnitType.Star) }
            },
            Children =
            {
                ActionButton,
                ChatHistoryScrollView 
            }
        };
    }

    private void InitializeChatFrame()
    {
        ChatHistoryFrame = new Frame
        {
            Content = ChatHistoryGrid
        };

        ChatHistoryFrame.DynamicResource(View.StyleProperty, "ChatHistoryFrame");
    }
}
