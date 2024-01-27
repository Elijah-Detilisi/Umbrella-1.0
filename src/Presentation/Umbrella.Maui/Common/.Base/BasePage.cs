﻿using System.Diagnostics;

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

    //Life-cylce
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        BindingContext.OnViewModelStarting();
        Debug.WriteLine($"OnBindingContextChanged: {Title}");
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        Debug.WriteLine($"OnAppearing: {Title}");
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        BindingContext.OnViewModelClosing();
        Debug.WriteLine($"OnDisappearing: {Title}");
    }
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
}