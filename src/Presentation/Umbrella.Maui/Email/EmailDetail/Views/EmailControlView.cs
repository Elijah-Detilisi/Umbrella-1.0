﻿namespace Umbrella.Maui.Email.EmailDetail.Views;

public class EmailControlView : ContentView
{
    //Fields
    private EmailModel EmailMessage { get; }
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //View components
    private static Grid? ContentGrid;

    private static Label? SentAtLabel;
    private static Label? SenderNameLabel;
    private static Image? SenderIconImage;
    private static ImageButton? ListenImageButton;
    private static ImageButton? RepeatImageButton;

    private static VerticalStackLayout? EmailDetailsLayout;
    private static HorizontalStackLayout? EmailControlsLayout;

    //Conctruction
    public EmailControlView(EmailModel emailMessage)
    {
        EmailMessage = emailMessage;

        InitializeView();
    }

    //Initialization
    protected virtual void InitializeView()
    {
        InitializeImages();
        InitializeLabels();
        InitializeEmailDetailsLayout();
        InitializeEmailControlsLayout();

        InitializeContentGrid();
        Content = ContentGrid;
    }

    //View component Initialization 
    private static void InitializeImages()
    {
        SenderIconImage = new()
        {
            WidthRequest = 50,
            HeightRequest = 50,
            Source = "circle_user_solid.svg"
        };

        ListenImageButton = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
            Source = "headphones_solid.svg"
        };

        RepeatImageButton = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
            Source = "repeat_solid.svg"
        };
    }
    private void InitializeLabels()
    {
        SentAtLabel = new()
        {
            MaxLines = 1,
            FontSize = 14,
            Text = EmailMessage.ModifiedAt.ToString("M"),
            FontAttributes = FontAttributes.Bold
        };

        SenderNameLabel = new()
        {
            MaxLines = 1,
            FontSize = 16,
            Text = EmailMessage.SenderName,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation,
        };
    }
    private static void InitializeEmailDetailsLayout()
    {
        EmailDetailsLayout = new()
        {
            Spacing = 5,
            Children =
            {
                SenderNameLabel,
                SentAtLabel
            }
        };
    }
    private static void InitializeEmailControlsLayout()
    {
        EmailControlsLayout = new()
        {
            Spacing = 10,
            Children =
            {
                ListenImageButton,
                RepeatImageButton
            }
        };
    }
    private static void InitializeContentGrid()
    {
        var leftColumn = 0.2;
        var rightColumn = 0.25;
        var centerColumn = 0.55;

        ContentGrid = new()
        {
            ColumnSpacing = 5,
            ColumnDefinitions =
            [
                new() { Width = new GridLength(leftColumn, GridUnitType.Star) },
                new() { Width = new GridLength(centerColumn, GridUnitType.Star) },
                new() { Width = new GridLength(rightColumn, GridUnitType.Star) },
            ],
            Children =
            {
                SenderIconImage?.Column(Column.Left),
                EmailDetailsLayout?.Column(Column.Center),
                EmailControlsLayout?.Column(Column.Right),
            }
        };
    }
}
