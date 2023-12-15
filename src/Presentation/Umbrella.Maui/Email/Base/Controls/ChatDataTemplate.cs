namespace Umbrella.Maui.Email.Base.Controls;

public class ChatDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Right = 1 }

    //View components 
    private static Label ChatTemplateText;
    private static Image ChatTemplateIcon;
    private static Frame ChatTemplateFrame;
    private static Grid ChatTemplateGrid;
    //Construction
    public ChatDataTemplate() : base(CreateTemplateGrid)
    {
        
    }

    //Initialization
    private static Grid CreateTemplateGrid()
    {
        InitializeChatText();
        InitializeChatIcon();
        InitializeChatFrame();
        InitializeChatGrid(); //Should be last to initialize
        
        return ChatTemplateGrid;
    }

    //View component Initialization 
    private static void InitializeChatText()
    {
        ChatTemplateText = new()
        {
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        ChatTemplateText.Bind(Label.TextProperty, 
            static (ChatMessageModel chat) => chat.Message, mode: BindingMode.OneTime
        );
    }

    private static void InitializeChatIcon()
    {
        ChatTemplateIcon = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
            Source = "user_solid.svg"
        };

        ChatTemplateIcon.Column(Column.Left); // NB: Must be in smallest column
    }

    private static void InitializeChatFrame()
    {
        ChatTemplateFrame = new(){ Content = ChatTemplateText };
        ChatTemplateFrame.DynamicResource(View.StyleProperty, "ChatTemplateFrame");

        ChatTemplateFrame.Column(Column.Right); // NB: Must be in biggest column
    }

    private static void InitializeChatGrid()
    {
        // Create DataTrigger
        var humanSpeakerTrigger = new DataTrigger(typeof(Grid))
        {
            Value = ChatSender.Human,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnDefinitionsProperty,
                    Value = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) }
                    }
                }
            }
        };

        var botSpeakerTrigger = new DataTrigger(typeof(Grid))
        {
            Value = ChatSender.Bot,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnDefinitionsProperty,
                    Value = new ColumnDefinitionCollection()
                    {
                        new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) },
                        new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) }
                    }
                }
            }
        };

        //Init
        ChatTemplateGrid = new Grid()
        {
            Padding = new Thickness(10),
            Children = { ChatTemplateIcon, ChatTemplateFrame },
            Triggers =
            {
                botSpeakerTrigger,
                humanSpeakerTrigger
            }
        };

    }
}
