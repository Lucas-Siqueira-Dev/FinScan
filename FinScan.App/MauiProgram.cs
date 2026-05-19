using Microsoft.Extensions.Logging;
using FinScan.App.Services;
using FinScan.App.ViewModels;
using FinScan.App.Views;
using Microcharts.Maui;

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

        // 1. Registra os Serviços (Regra de Negócio e APIs)
        // AddSingleton garante que teremos apenas um HttpClient rodando no app inteiro
        builder.Services.AddSingleton<ApiService>();

        // 2. Registra as ViewModels (A Lógica das Telas)
        // AddTransient cria uma nova ViewModel sempre que a tela for aberta
        builder.Services.AddTransient<SimulacaoViewModel>();

        // 3. Registra as Páginas (As Telas Físicas)
        builder.Services.AddTransient<MainPage>(); 
        
        builder.Services.AddTransient<SimulacaoPage>(); 

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