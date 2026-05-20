using FinScan.App.Services;

namespace FinScan.App.Views;

public partial class ScannerPage : ContentPage
{
    public ScannerPage()
    {
        InitializeComponent();
    }

    private async void TirarFoto_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
            
                FileResult foto = await MediaPicker.Default.CapturePhotoAsync(); 

                if (foto != null)
                {
                    string caminhoLocal = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                    
                    using (Stream streamOrigem = await foto.OpenReadAsync())
                    using (FileStream streamDestino = File.OpenWrite(caminhoLocal))
                    {
                        await streamOrigem.CopyToAsync(streamDestino);
                    }

                    var ocrService = new OcrService();
                    string textoBruto = await ocrService.ExtrairTextoDaImagemAsync(caminhoLocal);

                    var (nome, valor, data) = ReceiptAnalyzer.Analisar(textoBruto);

                    var parametros = new Dictionary<string, object>
                    {
                        { "NomeExt", nome },
                        { "ValorExt", valor },
                        { "DataExt", data }
                    };

                    await Shell.Current.GoToAsync("//SettingsPage", parametros);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Aviso", $"Captura cancelada ou falhou: {ex.Message}", "OK");
        }
    }
}