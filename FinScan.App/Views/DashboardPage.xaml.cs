using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class DashboardPage : ContentPage
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
            
            viewModel.CarregarDadosDaApi(); 
        }
    }
    private async void OnTransacaoTapped(object sender, TappedEventArgs e)
    {
        // 1. Pegamos o valor da transação que foi clicada
        if (e.Parameter is decimal valorGasto)
        {
            // 2. Pedimos ao MAUI para construir a página do Claude com todas as injeções prontas
            var simulacaoPage = Handler.MauiContext.Services.GetService<CustoOportunidadePage>();
        
            // 3. Pegamos a ViewModel dela e "injetamos" o valor do gasto, tirando o sinal de negativo
            var viewModel = (CustoOportunidadeViewModel)simulacaoPage.BindingContext;
            viewModel.ValorInicialText = Math.Abs(valorGasto).ToString("F2");
        
            // 4. Abrimos a tela como um Modal (sobreposição)
            await Navigation.PushModalAsync(simulacaoPage);
        }
    }
}