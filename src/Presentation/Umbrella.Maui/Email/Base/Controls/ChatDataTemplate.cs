namespace Umbrella.Maui.Email.Base.Controls;

public class ChatDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Right = 1 }

    //View components
    private readonly Grid ChatTemplateGrid;
    private readonly Label ChatTemplateText;
    private readonly Image ChatTemplateIcon;
    private readonly Frame ChatTemplateFrame;

    //Contruction
    public ChatDataTemplate()
    {
        ChatTemplateGrid = new Grid();
        ChatTemplateText = new Label();
        ChatTemplateIcon = new Image();
        ChatTemplateFrame = new Frame();

        InitializeTemplate();
    }

    //Initialization
    private void InitializeTemplate()
    {
        InitializeChatText();
        InitializeChatIcon();
        InitializeChatFrame();
        InitializeChatGrid();

        LoadTemplate = ()=>{ return ChatTemplateGrid; };
    }

    //View component Initialization 
    private void InitializeChatText()
    {
        ChatTemplateText.MaxLines = 1;
        ChatTemplateText.Text = "Hello world";
        ChatTemplateText.LineBreakMode = LineBreakMode.TailTruncation;
    }

    private void InitializeChatIcon()
    {
        ChatTemplateIcon.WidthRequest = 30;
        ChatTemplateIcon.HeightRequest = 30;
        ChatTemplateIcon.Source = "user_solid.svg";

        ChatTemplateIcon.Column(Column.Left); // NB: Must be in smallest column
    }

    private void InitializeChatFrame()
    {
        ChatTemplateFrame.Content = ChatTemplateText;
        ChatTemplateFrame.DynamicResource(View.StyleProperty, "ChatTemplateFrame");

        ChatTemplateFrame.Column(Column.Right); // NB: Must be in biggest column
    }

    private void InitializeChatGrid()
    {
        ChatTemplateGrid.Padding = new Thickness(10);
        ChatTemplateGrid.ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) }
        };

        ChatTemplateGrid.Children.Add(ChatTemplateIcon);
        ChatTemplateGrid.Children.Add(ChatTemplateFrame);
    }
}
