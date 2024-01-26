namespace Umbrella.Maui.Email.EmailListing.Templates;

public class EmailDataTemplate : DataTemplate
{
    //Fields
    private enum Column { Left = 0, Right = 1 }

    //View components 
    private static Grid? MainGrid;
    private static Image? ImageIcon;
    private static Grid? ContentGrid;
    private static Label? EmailTimeLabel;
    private static Label? EmailSenderLabel;
    private static Label? EmailSubjectLabel;
    private static BoxView? SeparaterBoxView;
    private static VerticalStackLayout? MainLayout;
    private static VerticalStackLayout? EmailTimeLayout;
    private static VerticalStackLayout? EmailDetailsLayout;
    
    //Construction
    public EmailDataTemplate() : base(CreateTemplate)
    {

    }

    //Initialization
    private static VerticalStackLayout? CreateTemplate()
    {
        InitializeImageIcon();
        InitializeSeparaterBoxView();
        InitializeEmailLabels();
        InitializeEmailDetailsLayout();
        InitializeEmailTimeLayout();
        InitializeMainGrid();
        InitializeMainLayout();// NB: Must be last to initialize

        return MainLayout;
    }

    //View component Initialization 
    private static void InitializeImageIcon()
    {
        ImageIcon = new()
        {
            WidthRequest = 30,
            HeightRequest = 30,
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

        SeparaterBoxView.DynamicResource(View.BackgroundColorProperty, "AppActionColor");
    }
    private static void InitializeEmailLabels()
    {
        //Setup
        EmailTimeLabel = new() {
            FontSize = 14 
        };
        
        EmailSubjectLabel = new()
        {
            MaxLines = 1, FontSize = 12,
            LineBreakMode = LineBreakMode.WordWrap
        };

        EmailSenderLabel = new()
        {
            MaxLines = 1, FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.TailTruncation
        };

        //Databings
        EmailTimeLabel.Bind(Label.TextProperty,
            static (EmailModel email) => email.CreatedAt, mode: BindingMode.OneTime, 
            stringFormat: "{}{0:h:mm tt}"
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
    }

    private static void InitializeEmailTimeLayout()
    {
        EmailTimeLayout = new()
        {
            Spacing = 5,
            Children =
            {
                EmailTimeLabel
            }
        };
    }

    private static void InitializeContentGrid()
    {
        var leftColumnSize = 1;
        var rightColumnSize = 0;

        ContentGrid = new()
        {
            ColumnDefinitions = 
            [
                new() { Width = new GridLength(leftColumnSize, GridUnitType.Star) },
                new (){ Width = new GridLength(rightColumnSize, GridUnitType.Auto) }
            ],
            Children =
            {
                EmailDetailsLayout?.Column(Column.Left),
                EmailTimeLayout?.Column(Column.Right)
            }
        };
    }

    private static void InitializeMainGrid()
    {
        var leftColumnSize = 0;
        var rightColumnSize = 1;

        MainGrid = new()
        {
            ColumnSpacing = 15,
            ColumnDefinitions = 
            [
                new() { Width = new GridLength(leftColumnSize, GridUnitType.Auto) },
                new() { Width = new GridLength(rightColumnSize, GridUnitType.Star) }
            ],
            Children =
            {
                ImageIcon?.Column(Column.Left),
                ContentGrid?.Column(Column.Right)
            }
        };
    }

    private static void InitializeMainLayout()
    {
        MainLayout = new()
        {
            Padding = new Thickness(10),
            Children =
            {
                MainGrid,
                SeparaterBoxView
            }
        };
    }
}
