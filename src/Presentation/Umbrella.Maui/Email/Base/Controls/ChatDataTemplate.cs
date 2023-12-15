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
        // Create DataTrigger
        var humanIconTrigger = new DataTrigger(typeof(Image))
        {
            Value = ChatSender.Human,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnProperty,
                    Value = (int)Column.Left
                }
            }
        };
        var botIconTrigger = new DataTrigger(typeof(Image))
        {
            Value = ChatSender.Bot,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnProperty,
                    Value = (int)Column.Right 
                }
            }
        };

        //Init
        ChatTemplateIcon = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
            Source = "user_solid.svg",
            Triggers =
            {
                humanIconTrigger, botIconTrigger
            }
        };
    }

    private static void InitializeChatFrame()
    {
        // Create DataTrigger
        var humanFrameTrigger = new DataTrigger(typeof(Frame))
        {
            Value = ChatSender.Human,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnProperty,
                    Value = (int)Column.Right // Left column for the Bot
                }
            }
        };
        var botFrameTrigger = new DataTrigger(typeof(Frame))
        {
            Value = ChatSender.Bot,
            Binding = new Binding(nameof(ChatMessageModel.Sender)),
            Setters =
            {
                new Setter()
                {
                    Property = Grid.ColumnProperty,
                    Value = (int)Column.Left // Left column for the Bot
                }
            }
        };

        //Init
        ChatTemplateFrame = new() 
        { 
            Content = ChatTemplateText,
            Triggers =
            {
                humanFrameTrigger, botFrameTrigger
            }
        };

        ChatTemplateFrame.DynamicResource(View.StyleProperty, "ChatTemplateFrame");

        //ChatTemplateFrame.Column(Column.Right); // NB: Must be in biggest column
    }

    private static void InitializeChatGrid()
    {
        // Create DataTrigger
        var humanGridTrigger = new DataTrigger(typeof(Grid))
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

        var botGridTrigger = new DataTrigger(typeof(Grid))
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
            Padding = new Thickness(15),
            Children = { ChatTemplateIcon, ChatTemplateFrame },
            Triggers =
            {
                botGridTrigger,
                humanGridTrigger
            }
        };

    }
}
