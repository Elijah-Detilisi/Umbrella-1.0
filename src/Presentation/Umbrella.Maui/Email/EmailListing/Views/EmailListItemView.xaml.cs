using Application.Email.Models;
using Domain.Common.ValueObjects;
using Domain.Email.ValueObjects;

namespace Umbrella.Maui.Email.EmailListing.Views;

public partial class EmailListItemView : ContentView
{
	public EmailListItemView()
	{
		InitializeComponent();
	}

    public EmailModel EmailMessage
    {
        get => (EmailModel)GetValue(EmailMessageProperty);
        set => SetValue(EmailMessageProperty, value);
    }
    
    public static readonly BindableProperty EmailMessageProperty = BindableProperty.Create(
        propertyName: nameof(EmailMessage),
        returnType: typeof(EmailModel),
        declaringType: typeof(EmailListItemView),
        defaultBindingMode: BindingMode.TwoWay,
        defaultValue: new EmailModel()
        {
            Sender = EmailAddress.Create("default@umbrella.com"),
            Subject = EmailSubjectLine.Create("Welcome to Umbrella - Your Ultimate Email Companion!"),
            Body = EmailBodyText.Create("Umbrella, your go-to app for all things email-related! " +
                "We are thrilled to have you on board and ready to make your daily weather experience more enjoyable and informative."),
            CreatedAt = DateTime.Now
        }
    );
}