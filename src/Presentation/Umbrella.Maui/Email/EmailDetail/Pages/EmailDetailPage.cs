using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailDetail.Views;

namespace Umbrella.Maui.Email.EmailDetail.Pages;

public class EmailDetailPage : EmailPage<EmailDetailViewModel>
{
    //View components
    private static Label? SubjectLabel;
    private static Label? BodyTextLabel;
    private EmailControlView? EmailControls;
    private static BoxView? SeparatorBoxView;
    
    //Construction
    public EmailDetailPage(EmailDetailViewModel viewModel, ChatHistoryView chatHistoryView)
        : base("", viewModel, chatHistoryView)
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
        EmailControls = new(BindingContext?.Email)
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
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior() {
            IconOverride = "back_solid.png"
        });

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
     
    private void InitializeSubjectLabel()
    {
        SubjectLabel = new()
        {
            Text = BindingContext?.Email?.Subject.Value
        };

        SubjectLabel.DynamicResource(View.StyleProperty, "EmailDetailPageSubjectLabel");
    }
    private void InitializeBodyTextLabel()
    {
        BodyTextLabel = new()
        {
            Text = BindingContext?.Email?.Body.Value
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
