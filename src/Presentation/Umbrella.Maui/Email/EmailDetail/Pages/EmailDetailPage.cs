using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailDetail.Views;

namespace Umbrella.Maui.Email.EmailDetail.Pages;

public class EmailDetailPage : EmailPage<EmailListingViewModel>
{
    //View components
    private static Label? SubjectLabel;
    private static Label? BodyTextLabel;
    private EmailControlView? EmailControls;
    private static BoxView? SeparatorBoxView;
    
    //Construction
    public EmailDetailPage(EmailListingViewModel viewModel, ChatHistoryView chatHistoryView)
        : base("EmailDetail", viewModel, chatHistoryView)
    {

    }

    protected override ScrollView PageContent
    {
        get
        {
            return new()
            {
                Padding = 10,
                Content = new VerticalStackLayout()
                {
                    SubjectLabel,
                    EmailControls,
                    SeparatorBoxView,
                    BodyTextLabel
                }
            };
        }
    }

    //View component Initialization
    protected override void InitializeEmailPage()
    {
        EmailControls = new(new EmailModel())
        {
            Margin = new Thickness(0, 20, 0, 20)
        };

        InitializeShell();
        InitializeSubjectLabel();
        InitializeBodyTextLabel();
        InitializeSeparatorBoxView();

        base.InitializeEmailPage();
    }

    private void InitializeShell()
    {
        Shell.SetBackgroundColor(this, Colors.Transparent);

        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "star_solid.png"
        });
        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "delete_solid.png"
        });
        ToolbarItems.Add(new ToolbarItem()
        {
            IconImageSource = "share_solid.png"
        });
    }
     
    private static void InitializeSubjectLabel()
    {
        SubjectLabel = new()
        {
            Text = "Subject: Catching Up Soon"
        };

        SubjectLabel.DynamicResource(View.StyleProperty, "EmailDetailPageSubjectLabel");
    }
    private static void InitializeBodyTextLabel()
    {
        BodyTextLabel = new()
        {
            Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In facilisis nulla eu felis fringilla vulputate. Nullam porta eleifend lacinia. Donec at iaculis tellus."
        };

        BodyTextLabel.DynamicResource(View.StyleProperty, "EmailDetailPageBodyTextLabel");
    }

    private static void InitializeSeparatorBoxView()
    {
        SeparatorBoxView = new()
        {
            Margin = new Thickness(10, 0, 10, 0)
        };
        SeparatorBoxView.DynamicResource(View.StyleProperty, "EmailDataTemplateSeparator");
    }
}
