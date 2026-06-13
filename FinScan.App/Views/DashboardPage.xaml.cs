using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class DashboardPage
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is DashboardViewModel viewModel)
        {
            _ = viewModel.CarregarDadosAsync(); 
        }
    }
    
    private async void OnTransacaoTapped(object sender, TappedEventArgs e)
    {
        try
        {
            // 1. Pegamos o valor da transação
            if (e.Parameter is decimal valorGasto)
            {
                // 2. Proteção (Null Reference): Garantimos que o Handler e o Contexto existem
                var servicos = Handler?.MauiContext?.Services;
                if (servicos == null) return; // Se a tela não estiver pronta, aborta silenciosamente

                // 3. Proteção (Null Reference): Garantimos que a página foi gerada com sucesso
                var simulacaoPage = servicos.GetService<CustoOportunidadePage>();
                if (simulacaoPage == null) return;

                // 4. Proteção de Tipo: Pega a ViewModel com segurança usando 'is'
                if (simulacaoPage.BindingContext is CustoOportunidadeViewModel viewModel)
                {
                    viewModel.ValorInicialText = Math.Abs(valorGasto).ToString("F2");
                }
        
                // 5. Abrimos a tela como um Modal
                await Navigation.PushModalAsync(simulacaoPage);
            }
        }
        catch (Exception ex)
        {
            // Se qualquer coisa bizarra acontecer, o app não crasha. Fica apenas o registro no log.
            Console.WriteLine($"Falha silenciosa ao tentar abrir a simulação: {ex.Message}");
        }
    }
}