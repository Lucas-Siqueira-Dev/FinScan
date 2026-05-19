using FinScan.App.ViewModels;
using Microsoft.Maui.Controls;

namespace FinScan.App;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}