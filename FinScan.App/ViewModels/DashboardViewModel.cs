using System.Collections.ObjectModel;
using FinScan.App.Services; // Para enxergar o ApiService

namespace FinScan.App.ViewModels;

public class DashboardViewModel : BindableObject
{
    private readonly ApiService _apiService;

    private decimal _saldoDisponivel;
    public decimal SaldoDisponivel
    {
        get => _saldoDisponivel;
        set { _saldoDisponivel = value; OnPropertyChanged(); }
    }

    public string SaudacaoUsuario => "Olá, Lucas"; 

    public ObservableCollection<Leitura> UltimasLeituras { get; set; } = new();

    // Injetamos o ApiService no construtor
    public DashboardViewModel(ApiService apiService)
    {
        _apiService = apiService;
        CarregarDadosAsync();
    }

    public async Task CarregarDadosAsync()
    {
        UltimasLeituras.Clear();
        
        // 1. Vai na API do Render puxar os dados do Supabase
        var dadosDaApi = await _apiService.GetDashboardDataAsync();

        decimal totalGasto = 0;

        // 2. Transforma o que veio da API no formato visual da tela
        if (dadosDaApi != null)
        {
            foreach (var item in dadosDaApi)
            {
                UltimasLeituras.Add(new Leitura
                {
                    NomeServico = item.Category, // Usa a categoria que o backend gerou
                    Valor = item.Total,
                    IconeCategoria = "receipt_icon.png" // Ícone genérico
                });
                totalGasto += item.Total;
            }
        }

        // Simula um saldo base (ex: 5000) menos os gastos reais
        SaldoDisponivel = 5000.00m - totalGasto;
    }
    
    public class Leitura
    {
        public string NomeServico { get; set; }
        public decimal Valor { get; set; }
        public string IconeCategoria { get; set; }
    }
}