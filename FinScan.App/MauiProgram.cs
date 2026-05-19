using FinScan.App.Services;
using FinScan.App.ViewModels;
using FinScan.App.Views;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;

namespace FinScan.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .UseMicrocharts() 
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        
#if DEBUG
        builder.Logging.AddDebug();
#endif

    
        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddTransient<SimulacaoViewModel>();
        builder.Services.AddTransient<SimulacaoPage>();
        builder.Services.AddTransient<DashboardViewModel>();
        builder.Services.AddTransient<DashboardPage>();
 
        return builder.Build();
    }
}