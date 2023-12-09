using CommunityToolkit.Maui.Markup;
using System.Diagnostics;

namespace Umbrella.Maui.Email.Base.Pages;

// Content type
public abstract class EmailPage : ContentPage
{
    private enum Row { Content = 0, DialogueBox = 1 }

    protected EmailPage(object? viewModel = null)
    {
        BindingContext = viewModel;
        
        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }

        Content = new Grid
        {
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = new GridLength(0.8, GridUnitType.Star) },
                new RowDefinition { Height = new GridLength(0.3, GridUnitType.Star) }
            },
            Children =
            {
                //Content
                new VerticalStackLayout
                {

                }.Row(Row.Content),

                //DialogueBox
                new Frame
                {
                    CornerRadius = 40,
                    Margin = new Thickness(-5, -25, -5, -25),
                    BackgroundColor = Color.FromRgb(200,100,30),
                    Content = new Grid
                    {
                        Children =
                        {
                            new VerticalStackLayout()
                        }
                    }
                }.Row(Row.DialogueBox),
            }
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine($"OnAppearing: {Title}");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Debug.WriteLine($"OnDisappearing: {Title}");
    }
}