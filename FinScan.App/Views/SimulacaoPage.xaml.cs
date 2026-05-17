using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class SimulacaoPage : ContentPage
{
    public SimulacaoPage(SimulacaoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}