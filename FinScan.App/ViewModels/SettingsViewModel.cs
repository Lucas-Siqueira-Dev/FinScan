using System.Windows.Input;
using FinScan.App.Services;

namespace FinScan.App.ViewModels;

[QueryProperty(nameof(NomeEstabelecimento), "NomeExt")]
[QueryProperty(nameof(ValorTotal), "ValorExt")]
[QueryProperty(nameof(DataEmissao), "DataExt")]
public class SettingsViewModel : BindableObject
{
    private string _nomeEstabelecimento;
    public string NomeEstabelecimento
    {
        get => _nomeEstabelecimento;
        set { _nomeEstabelecimento = value; OnPropertyChanged(); }
    }

    private decimal _valorTotal;
    public decimal ValorTotal
    {
        get => _valorTotal;
        set { _valorTotal = value; OnPropertyChanged(); }
    }

    private string _dataEmissao;
    public string DataEmissao
    {
        get => _dataEmissao;
        set { _dataEmissao = value; OnPropertyChanged(); }
    }

    private string _categoriaSelecionada;
    public string CategoriaSelecionada
    {
        get => _categoriaSelecionada;
        set { _categoriaSelecionada = value; OnPropertyChanged(); }
    }

    private readonly ApiService _apiService;

    public SettingsViewModel()
    {
        CategoriaSelecionada = "Selecione";
        _apiService = new ApiService(); // Conecta na API do Render
    }
    
    public async Task SalvarNotaAsync()
    {
        // Alerta de teste de vida!
        await Shell.Current.DisplayAlert("Aviso", "Iniciando envio para o Render...", "OK");

        bool sucesso = await _apiService.SalvarNotaFiscalAsync(NomeEstabelecimento, ValorTotal, DataEmissao);

        if (sucesso)
        {
            await Shell.Current.DisplayAlert("Sucesso", "Nota fiscal salva no banco de dados!", "OK");
            await Shell.Current.GoToAsync("//DashboardPage");
        }
        else
        {
            await Shell.Current.DisplayAlert("Erro", "Falha ao conectar com o servidor.", "OK");
        }
    }
}
