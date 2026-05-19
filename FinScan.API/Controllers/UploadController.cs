using Microsoft.AspNetCore.Mvc;
using FinScan.API.Services;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FinScan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IOcrService _ocrService;

        // O Controller agora depende da abstração, não da implementação (DIP)
        public UploadController(IOcrService ocrService)
        {
            _ocrService = ocrService;
        }

        [HttpPost("processar-nota")]
        public async Task<IActionResult> ProcessarNota(IFormFile arquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest(new { mensagem = "Nenhum arquivo enviado." });
            }

            try
            {
                // Salva o arquivo temporariamente para que o motor do OCR possa ler do disco
                var caminhoTemporario = Path.Combine(Path.GetTempPath(), arquivo.FileName);
                using (var stream = new FileStream(caminhoTemporario, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }

                // A responsabilidade de extrair o texto é totalmente do serviço (SRP)
                string textoExtraido = await _ocrService.ExtrairTextoDaImagemAsync(caminhoTemporario);

                // Limpeza do arquivo temporário após o processamento
                if (System.IO.File.Exists(caminhoTemporario))
                {
                    System.IO.File.Delete(caminhoTemporario);
                }

                return Ok(new { texto = textoExtraido });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = $"Erro ao processar o upload: {ex.Message}" });
            }
        }
    }
}