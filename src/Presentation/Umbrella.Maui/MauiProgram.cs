using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Umbrella.Maui.Email.EmailListing.Pages;

namespace Umbrella.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("RobotoFlex-Regular.ttf", "RobotoFlex-Regular");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<EmailListingPage, EmailListingViewModel>();

            return builder.Build();
        }
    }
}
