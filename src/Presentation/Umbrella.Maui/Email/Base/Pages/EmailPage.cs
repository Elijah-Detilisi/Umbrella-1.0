using Umbrella.Maui.Email.Base.Controls;

namespace Umbrella.Maui.Email.Base.Pages;

public abstract class EmailPage<TViewModel> : BasePage<TViewModel> where TViewModel : ViewModel
{
    private enum Row { Content = 0, DialogueBox = 1 }
    protected abstract ScrollView ContentView { get; }

    protected EmailPage(string title, TViewModel viewModel) : base(viewModel)
    {
        Padding = 0;
        Title = title;
        
        InitializeContent();
    }

    private void InitializeContent()
    {
        Content = new Grid
        {
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
            },
            Children =
            {
                //Content
                ContentView.Row(Row.Content),

                //DialogueBox
                new Frame
                {
                    Content = new ScrollView
                    {
                        Content = new Grid()
                        {
                            Padding = 10,
                            RowDefinitions = new RowDefinitionCollection
                            {
                                new RowDefinition { Height = new GridLength(0.7, GridUnitType.Star) },
                                new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
                            },
                            Children =
                            {
                                new VerticalStackLayout()
                                {
                                    new SpeechBubbleControl(isBotSpeaker: true, message: "Hello world"),
                                    new SpeechBubbleControl(isBotSpeaker: false, message: "What's up?"),
                                }.Row(0),
                                new ImageButton()
                                {
                                    WidthRequest = 40,
                                    HeightRequest = 40,
                                    Source = "umbrella_solid.svg"
                                }.Row(1)
                            }
                        }
                    }
                }.DynamicResource(View.StyleProperty, "FrameDialogueBox").Row(Row.DialogueBox),
            }
        };
    }
}