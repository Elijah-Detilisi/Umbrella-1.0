using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Layouts;

namespace Umbrella.Maui.Email.EmailListing.Templates;

public class EmailDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Center = 1, Right = 2 }

    //View components
    private static Image? ImageIcon;
    private static Grid? ContentGrid;
    private static Label? EmailTimeLabel;
    private static Label? EmailSenderLabel;
    private static Label? EmailSubjectLabel;
    private static BoxView? SeparaterBoxView;
    private static VerticalStackLayout? EmailDetailsLayout;

    //Construction
    public EmailDataTemplate() : base(CreateTemplate)
    {

    }

    //Initialization
    private static DockLayout? CreateTemplate()
    {
        InitializeImageIcon();
        InitializeSeparaterBoxView();
        InitializeEmailLabels();
        InitializeEmailDetailsLayout();
        InitializeContentGrid();

        return new()
        {
            Padding = new Thickness(10),
            Children =
            {
                ContentGrid,
                SeparaterBoxView
            }
        };
    }

    //View component Initialization 
    private static void InitializeImageIcon()
    {
        ImageIcon = new()
        {
            WidthRequest = 50,
            HeightRequest = 50,
            Source = "envelope_solid.svg"
        };
    }
    private static void InitializeSeparaterBoxView()
    {
        SeparaterBoxView = new()
        {
            HeightRequest = 2,
            Margin = new Thickness(0, 15, 0, 0),
            HorizontalOptions = LayoutOptions.Fill
        };

        DockLayout.SetDockPosition(SeparaterBoxView, DockPosition.Bottom);
        SeparaterBoxView.DynamicResource(View.BackgroundColorProperty, "AppActionColor");
    }
    private static void InitializeEmailLabels()
    {
        //Setup
        EmailTimeLabel = new();

        EmailSubjectLabel = new()
        {
            MaxLines = 1,
            FontSize = 12,
            LineBreakMode = LineBreakMode.WordWrap
        };

        EmailSenderLabel = new()
        {
            MaxLines = 1,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation,
        };

        //Databings
        EmailTimeLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.CreatedAt, mode: BindingMode.OneTime,
            stringFormat: "{0:h:mm tt}"
        );

        EmailSubjectLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.Subject.Value, mode: BindingMode.OneTime
        );

        EmailSenderLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.Sender.Value, mode: BindingMode.OneTime
        );
    }
    private static void InitializeEmailDetailsLayout()
    {
        EmailDetailsLayout = new()
        {
            Spacing = 5,
            Children =
            {
                EmailSenderLabel,
                EmailSubjectLabel
            }
        };

        DockLayout.SetDockPosition(EmailDetailsLayout, DockPosition.Top);
    }
    private static void InitializeContentGrid()
    {
        var centerColumn = 0.6;
        var lateralColumn = 0.2;

        ContentGrid = new()
        {
            ColumnSpacing = 5,
            ColumnDefinitions =
            [
                new() { Width = new GridLength(lateralColumn, GridUnitType.Star) },
                new() { Width = new GridLength(centerColumn, GridUnitType.Star) },
                new() { Width = new GridLength(lateralColumn, GridUnitType.Star) },
            ],
            Children =
            {
                ImageIcon?.Column(Column.Left),
                EmailDetailsLayout?.Column(Column.Center),
                EmailTimeLabel?.Column(Column.Right),
            }
        };
    }
}
