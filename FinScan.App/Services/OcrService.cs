using Plugin.Maui.OCR;

namespace FinScan.App.Services;

public class OcrService
{
    public async Task<string> ExtrairTextoDaImagemAsync(string caminhoImagemLocal)
    {
        try
        {
            byte[] bytesDaImagem = await File.ReadAllBytesAsync(caminhoImagemLocal);
            
            await OcrPlugin.Default.InitAsync();
            
            var resultado = await OcrPlugin.Default.RecognizeTextAsync(bytesDaImagem);

            if (resultado.Success)
            {
                return resultado.AllText; 
            }

            return "Nenhum texto identificado na imagem.";
        }
        catch (Exception ex)
        {
            return $"Erro ao ler a nota: {ex.Message}";
        }
    }
}