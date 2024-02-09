﻿using Umbrella.Maui.Email.Base.Pages;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailDetail.Views;

namespace Umbrella.Maui.Email.EmailDetail.Pages;

public class EmailDetailPage : EmailPage<EmailListingViewModel>
{
    //View components
    private static Label? SubjectLabel;
    private EmailControlView EmailControlView;
    
    //Construction
    public EmailDetailPage(EmailListingViewModel viewModel, ChatHistoryView chatHistoryView)
        : base("EmailDetail", viewModel, chatHistoryView)
    {
        InitializeShell();
        
    }

    //Initialization
    protected override ScrollView PageContent {
        get 
        {
            InitializeSubjectLabel();
            EmailControlView = new(new EmailModel());

            return new()
            {
                Content = new VerticalStackLayout()
                {
                    SubjectLabel,
                    EmailControlView
                }
            };
        }
    }

    //View component Initialization
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

}
