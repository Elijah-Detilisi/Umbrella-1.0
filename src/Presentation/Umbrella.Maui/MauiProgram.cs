﻿using Application.Chat.ViewModels;
using Application.Common.Services;
using Application.Email.Services;
using CommunityToolkit.Maui;
using Infrastructure.Common.Service;
using Infrastructure.Email.Services;
using Microsoft.Extensions.Logging;
using Umbrella.Maui.Email.Base.Views;
using Umbrella.Maui.Email.EmailDetail.Pages;
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
            //Views
            builder.Services.AddSingleton<ChatHistoryView, ChatViewModel>();

            //Service
            builder.Services.AddSingleton<IEmailFetcher, EmailFetcher>();

            builder.Services.AddSingleton<IAppTextToSpeech, AppTextToSpeech>();
            builder.Services.AddSingleton<IAppSpeechRecognition, AppSpeechRecognition>();

            //ViewModel
            builder.Services.AddTransient<EmailListingPage, EmailListingViewModel>();
            builder.Services.AddTransient<EmailDetailPage, EmailDetailViewModel>();

            return builder.Build();
        }
    }
}
