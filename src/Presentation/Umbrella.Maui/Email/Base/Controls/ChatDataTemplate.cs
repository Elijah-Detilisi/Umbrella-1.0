using Umbrella.Maui.Email.Base.Triggers;

namespace Umbrella.Maui.Email.Base.Controls;

public class ChatDataTemplate : DataTemplate
{
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
            Source = "user_solid.svg",
            Triggers =
            {
                ChatTemplateIconTriggers.HumanSenderTrigger, 
                ChatTemplateIconTriggers.BotSenderTrigger
            }
        };
    }

    private static void InitializeChatFrame()
    {
        ChatTemplateFrame = new() 
        { 
            Content = ChatTemplateText,
            Triggers =
            {
                ChatTemplateFrameTriggers.HumanSenderTrigger, 
                ChatTemplateFrameTriggers.BotSenderTrigger
            }
        };

        ChatTemplateFrame.DynamicResource(View.StyleProperty, "ChatTemplateFrame");
    }

    private static void InitializeChatGrid()
    {
        ChatTemplateGrid = new Grid()
        {
            Padding = new Thickness(15),
            Children = { ChatTemplateIcon, ChatTemplateFrame },
            Triggers =
            {
                ChatTemplateGridTriggers.BotSenderTrigger,
                ChatTemplateGridTriggers.HumanSenderTrigger
            }
        };
    }
}
