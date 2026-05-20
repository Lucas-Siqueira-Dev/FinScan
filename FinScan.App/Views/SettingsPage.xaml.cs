using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    // O botão da tela vai bater direto aqui agora
    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        // Pega o nosso ViewModel que está conectado na tela
        if (BindingContext is SettingsViewModel viewModel)
        {
            // Força a execução do método de salvar!
            await viewModel.SalvarNotaAsync();
        }
    }
}