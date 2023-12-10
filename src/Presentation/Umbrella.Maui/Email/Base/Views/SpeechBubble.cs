﻿using CommunityToolkit.Maui.Views;

namespace Umbrella.Maui.Email.Base.Views;

public class SpeechBubble : Grid
{
    public SpeechBubble()
    {
        InitializeLayout();
        InitializeChildern();
    }

    private void InitializeLayout()
    {
        Padding = new Thickness(10);
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new ColumnDefinition { Width = new GridLength(0.2, GridUnitType.Star) },
            new ColumnDefinition { Width = new GridLength(0.8, GridUnitType.Star) }
        };
    }

    private void InitializeChildern()
    {
        //Speaker icon
        Children.Add(new Image
        {
            Source = "user_solid.svg",
            WidthRequest = 30,
            HeightRequest = 30,
            VerticalOptions = LayoutOptions.Center
        }.Column(0)
        );

        //Message box
        Children.Add(new Border
        {
            BackgroundColor = Colors.AliceBlue,
            Padding = 10,
            Content = new Label
            {
                Text = "Hello! This is a speech bubble with an image.",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand
            }
        }.Column(1)
        );
    }
}
