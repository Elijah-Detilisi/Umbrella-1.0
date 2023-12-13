using System.Diagnostics;

namespace Umbrella.Maui.Common.Base;

/// <summary>
/// BasePage ViewModel-bound Type
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
public abstract class BasePage<TViewModel> : BasePage where TViewModel : ViewModel
{
    //Construction
    protected BasePage(TViewModel viewModel) : base(viewModel)
    {
    }

    //Properties
    public new TViewModel BindingContext => (TViewModel)base.BindingContext;
}

/// <summary>
/// BasePage Content Type
/// </summary>
public abstract class BasePage : ContentPage
{
    //Construction
    protected BasePage(object? viewModel = null)
    {
        BindingContext = viewModel;
        Padding = 12;

        if (string.IsNullOrWhiteSpace(Title))
        {
            Title = GetType().Name;
        }
    }

    //Life-cylce
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