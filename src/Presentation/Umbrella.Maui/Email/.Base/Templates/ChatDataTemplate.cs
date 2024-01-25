using Umbrella.Maui.Email.Base.Triggers;

namespace Umbrella.Maui.Email.Base.Templates;

public class ChatDataTemplate : DataTemplate
{
    //View components 
    private static Grid? MainGrid;
    private static Label? TextLabel;
    private static Image? ImageIcon;
    private static Frame? ContentFrame;
    
    //Construction
    public ChatDataTemplate() : base(CreateTemplateGrid)
    {
        
    }

    //Initialization
    private static Grid? CreateTemplateGrid()
    {
        InitializeTextLabel();
        InitializeImageIcon();
        InitializeContentFrame();
        InitializeChatGrid(); //Should be last to initialize
        
        return MainGrid;
    }

    //View component Initialization 
    private static void InitializeTextLabel()
    {
        TextLabel = new()
        {
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        TextLabel.Bind(Label.TextProperty, 
            static (ChatMessageModel chat) => chat.Message, mode: BindingMode.OneTime
        );
    }

    private static void InitializeImageIcon()
    {
        ImageIcon = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
            Source = "user_solid.svg",
            Triggers =
            {
                ChatTemplateIconTriggers.HumanSenderTrigger, 
                ChatTemplateIconTriggers.BotSenderTrigger
            }
        };
    }

    private static void InitializeContentFrame()
    {
        ContentFrame = new() 
        { 
            Content = TextLabel,
            Triggers =
            {
                ChatTemplateFrameTriggers.HumanSenderTrigger, 
                ChatTemplateFrameTriggers.BotSenderTrigger
            }
        };

        ContentFrame.DynamicResource(View.StyleProperty, "ChatDataTemplateContentFrame");
    }

    private static void InitializeChatGrid()
    {
        MainGrid = new Grid()
        {
            Padding = new Thickness(15),
            Children = { ImageIcon, ContentFrame },
            Triggers =
            {
                ChatTemplateGridTriggers.BotSenderTrigger,
                ChatTemplateGridTriggers.HumanSenderTrigger
            }
        };
    }
}
