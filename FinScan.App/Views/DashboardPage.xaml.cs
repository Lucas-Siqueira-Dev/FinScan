using FinScan.App.ViewModels;

namespace FinScan.App.Views;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(DashboardViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}