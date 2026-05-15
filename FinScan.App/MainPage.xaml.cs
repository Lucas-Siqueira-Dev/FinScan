using Microcharts;
using SkiaSharp;
using FinScan.App.Services;

namespace FinScan.App;

public partial class MainPage : ContentPage
{
    private readonly ApiService _apiService;

    public MainPage()
    {
        InitializeComponent();
        _apiService = new ApiService();
        LoadData();
    }

    private void LoadData()
    {
        // Task FSVS-29: Integrando os dados com a interface do Dashboard
        var data = new[]
        {
            new ChartEntry(450) { Label = "Alimentação", ValueLabel = "450", Color = SKColor.Parse("#FF5733") },
            new ChartEntry(300) { Label = "Transporte", ValueLabel = "300", Color = SKColor.Parse("#33FF57") },
            new ChartEntry(150) { Label = "Lazer", ValueLabel = "150", Color = SKColor.Parse("#3357FF") }
        };

        chartView.Chart = new DonutChart { Entries = data, LabelTextSize = 35 };
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        LoadData();
    }
}