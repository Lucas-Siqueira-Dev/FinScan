using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class CustoOportunidadePage : ContentPage
{
    public CustoOportunidadePage(CustoOportunidadeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}