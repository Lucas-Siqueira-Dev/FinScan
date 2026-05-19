using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinScan.App.Services;
using Microcharts;
using SkiaSharp;

namespace FinScan.App.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty]
    private Chart chart;

    public MainViewModel(ApiService apiService)
    {
        _apiService = apiService;
        CarregarDados();
    }

    [RelayCommand]
    public void CarregarDados()
    {
       
        var data = new[]
        {
            new ChartEntry(450) { Label = "Alimentação", ValueLabel = "450", Color = SKColor.Parse("#FF5733") },
            new ChartEntry(300) { Label = "Transporte", ValueLabel = "300", Color = SKColor.Parse("#33FF57") },
            new ChartEntry(150) { Label = "Lazer", ValueLabel = "150", Color = SKColor.Parse("#3357FF") }
        };

        Chart = new DonutChart { Entries = data, LabelTextSize = 35 };
    }
}