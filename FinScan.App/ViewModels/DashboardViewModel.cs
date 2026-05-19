using System.Collections.ObjectModel;

namespace FinScan.App.ViewModels;

public class DashboardViewModel : BindableObject
{
    // O MAUI "escuta" essa variável. Se o saldo mudar, a tela atualiza sozinha!
    private decimal _saldoDisponivel;
    public decimal SaldoDisponivel
    {
        get => _saldoDisponivel;
        set
        {
            _saldoDisponivel = value;
            OnPropertyChanged();
        }
    }

    public string SaudacaoUsuario => "Olá Guilherme";

    // ObservableCollection é uma lista especial para o front-end. 
    // Quando você adicionar uma nota nova aqui, ela aparece na tela instantaneamente.
    public ObservableCollection<Leitura> UltimasLeituras { get; set; } = new();

    public DashboardViewModel()
    {
        // Aqui simulamos a chamada da sua API. 
        // No futuro, você vai injetar o ApiService aqui para buscar do MySQL.
        CarregarDadosDaApi();
    }

    private void CarregarDadosDaApi()
    {
        SaldoDisponivel = 2450.00m;

        UltimasLeituras.Add(new Leitura 
        { 
            NomeServico = "Montagem de Móveis -", 
            Valor = 2000.00m, 
            IconeCategoria = "settings_icon.png" // Usando os ícones que você já tem
        });
        
        UltimasLeituras.Add(new Leitura 
        { 
            NomeServico = "Limpeza da Obra -", 
            Valor = 800.00m, 
            IconeCategoria = "home_icon.png" 
        });
        
        UltimasLeituras.Add(new Leitura 
        { 
            NomeServico = "Materiais de Pintura -", 
            Valor = 2000.00m, 
            IconeCategoria = "camera_icon.png" 
        });
    }
}

// Uma classe simples de Model para representar as notas fiscais escaneadas
public class Leitura
{
    public string NomeServico { get; set; }
    public decimal Valor { get; set; }
    public string IconeCategoria { get; set; }
}