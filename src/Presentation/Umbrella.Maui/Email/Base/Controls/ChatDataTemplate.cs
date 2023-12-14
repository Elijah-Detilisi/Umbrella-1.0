using Application.Chat.Models;

namespace Umbrella.Maui.Email.Base.Controls;

public class ChatDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Right = 1 }

    //View components 
    private static Label ChatTemplateText;
    private static Image ChatTemplateIcon;
    private static Frame ChatTemplateFrame;

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

        return new Grid()
        {
            Padding = new Thickness(10),
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) }
            },
            Children =
            {
                ChatTemplateIcon, 
                ChatTemplateFrame
            }
        };
    }

    //View component Initialization 
    private static void InitializeChatText()
    {
        ChatTemplateText = new()
        {
            MaxLines = 1,
            Text = "Hello world",
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
}
