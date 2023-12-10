namespace Umbrella.Maui.Email.Base.Views;

public class SpeechBubble : Grid
{
    public SpeechBubble(bool isBotSpeaker)
    {
        InitializeLayout(isBotSpeaker);
        InitializeChildern(isBotSpeaker);
    }

    private void InitializeLayout(bool isBotSpeaker)
    {
        Padding = new Thickness(10);
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition { Width = new GridLength( isBotSpeaker? 0.2 : 0.8, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(isBotSpeaker? 0.8 : 0.2, GridUnitType.Star) }
        };
    }

    private void InitializeChildern(bool isBotSpeaker)
    {
        //Speaker icon
        Children.Add(new Image
        {
            Source = "user_solid.svg",
            WidthRequest = 30, HeightRequest = 30,
        }.Column(isBotSpeaker? 0 : 1)
        );

        //Message box
        Children.Add(new Frame
        {
            Content = new Label
            {
                Text = "Hello! This is a speech bubble with an image.",
            }
        }.DynamicResource(View.StyleProperty, "FrameSpeechBox").Column(isBotSpeaker ? 1 : 0)
        );;
    }
}
