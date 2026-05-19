namespace FinScan.App.Views;

public partial class ScannerPage : ContentPage
{
    public ScannerPage()
    {
        InitializeComponent();
    }

    // Essa é a função que o XAML estava procurando!
    private async void TirarFoto_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            // 1. Verifica se o celular (ou emulador) tem uma câmera disponível
            if (MediaPicker.Default.IsCaptureSupported)
            {
                // 2. Abre a câmera nativa e pausa o aplicativo esperando a foto
                FileResult foto = await MediaPicker.Default.CapturePhotoAsync();

                // 3. Verifica se o usuário tirou a foto (se ele não fechou a câmera sem tirar)
                if (foto != null)
                {
                    // 4. Salva a imagem temporariamente no cache do celular
                    string caminhoLocal = Path.Combine(FileSystem.CacheDirectory, foto.FileName);
                    
                    using Stream streamOrigem = await foto.OpenReadAsync();
                    using FileStream streamDestino = File.OpenWrite(caminhoLocal);
                    
                    await streamOrigem.CopyToAsync(streamDestino);
                    
                    await Shell.Current.GoToAsync("//SettingsPage");
                }
            }
            else
            {
                await DisplayAlert("Aviso", "Seu dispositivo não suporta captura de fotos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Falha ao abrir a câmera: {ex.Message}", "OK");
        }
    }
}