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
            Subject = EmailSubjectLine.Create("Welcome to Umbrella - Your Ultimate Email Companion!")
        }
    );
}