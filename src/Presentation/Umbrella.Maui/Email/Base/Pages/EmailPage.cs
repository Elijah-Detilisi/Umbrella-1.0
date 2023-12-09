namespace Umbrella.Maui.Email.Base.Pages;

public abstract class EmailPage<TViewModel> : BasePage<TViewModel> where TViewModel : ViewModel
{
    private enum Row { Content = 0, DialogueBox = 1 }

    protected EmailPage(string title, TViewModel viewModel) : base(viewModel)
    {
        Padding = 0;
        Title = title;
        
        InitializeContent();
    }

    private void InitializeContent()
    {
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
}