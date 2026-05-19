using System;
using System.Threading.Tasks;
using Tesseract;

namespace FinScan.API.Services
{
    public class OcrService : IOcrService
    {
        public Task<string> ExtrairTextoDaImagemAsync(string caminhoImagem)
        {
            return Task.Run(() =>
            {
                try
                {
                    // Lembre-se de verificar se o caminho do tessdata está correto para o ambiente de vocês
                    using var engine = new TesseractEngine(@"./tessdata", "por", EngineMode.Default);
                    using var img = Pix.LoadFromFile(caminhoImagem);
                    using var page = engine.Process(img);
                    
                    return page.GetText();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao processar imagem no OCR: {ex.Message}");
                }
            });
        }
    }
}