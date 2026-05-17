using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinScan.App.Services;
using FinScan.App.Models;

namespace FinScan.App.ViewModels;

public partial class SimulacaoViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    [ObservableProperty] private decimal valorInicial;
    [ObservableProperty] private decimal aporteMensal;
    [ObservableProperty] private int tempoMeses;
    [ObservableProperty] private decimal resultadoFinal;
    [ObservableProperty] private bool isBusy;

    public SimulacaoViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    [RelayCommand]
    public async Task CalcularSimulacaoAsync()
    {
        if (ValorInicial < 0 || AporteMensal < 0 || TempoMeses <= 0)
        {
            await Shell.Current.DisplayAlert("Aviso", "Preencha com valores válidos.", "OK");
            return;
        }

        IsBusy = true;

        try
        {
            var request = new SimulacaoRequest
            {
                ValorInicial = this.ValorInicial,
                AporteMensal = this.AporteMensal,
                TempoMeses = this.TempoMeses
            };

            var resultado = await _apiService.CalcularRentabilidadeAsync(request);
            if (resultado != null) ResultadoFinal = resultado.ValorTotal;
        }
        catch (Exception)
        {
            await Shell.Current.DisplayAlert("Erro", "Falha ao conectar na API.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}