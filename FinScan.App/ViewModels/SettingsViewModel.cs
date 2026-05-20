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

    public SettingsViewModel()
    {
        CategoriaSelecionada = "Selecione";
    }
}