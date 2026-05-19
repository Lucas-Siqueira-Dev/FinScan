namespace FinScan.App.ViewModels;

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

    public SettingsViewModel()
    {
        // Textos temporários só para testarmos se a tela está recebendo os dados
        NomeEstabelecimento = "Aguardando leitura...";
        ValorTotal = 0.00m;
        DataEmissao = DateTime.Now.ToString("dd/MM/yyyy");
        CategoriaSelecionada = "Selecione";
    }
}