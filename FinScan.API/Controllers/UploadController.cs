using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;
using Tesseract;
using System.Text.RegularExpressions;

namespace FinScan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> UploadComprovante(IFormFile file)
    {
        // Validação inicial para garantir que um arquivo foi enviado
        if (file == null || file.Length == 0)
        {
            return BadRequest("Nenhum arquivo ou foto enviada.");
        }

        try
        {
            // 1. Converte a foto (IFormFile) recebida na API para um array de bytes em memória
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            byte[] imageBytes = memoryStream.ToArray();

            // 2. Inicia o motor do Tesseract 
            // O caminho "./tessdata" aponta para a pasta onde você colocou o arquivo "por.traineddata"
            using var engine = new TesseractEngine(@"./tessdata", "por", EngineMode.Default);
            
            // 3. Carrega a imagem e manda o Tesseract ler
            using var img = Pix.LoadFromMemory(imageBytes);
            using var page = engine.Process(img);
            
            // 4. Extrai o Texto Bruto (Raw Text)
            string textoBruto = page.GetText();

            // 5. Pesca o CNPJ usando Regex (Task FSVS-38)
            string padraoCnpj = @"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}";
            Match matchCnpj = Regex.Match(textoBruto, padraoCnpj);
            string cnpjEncontrado = matchCnpj.Success ? matchCnpj.Value : "Não encontrado";

            // 6. Pesca a Data e o Valor Total (Task FSVS-39)
            // Procura pelo padrão DD/MM/AAAA
            string padraoData = @"\d{2}/\d{2}/\d{4}";
            Match matchData = Regex.Match(textoBruto, padraoData);
            string dataEncontrada = matchData.Success ? matchData.Value : "Não encontrada";

            // Procura por "R$" seguido de espaços e números com vírgula (Ex: R$ 2,00 ou R$ 1.500,00)
            string padraoValor = @"R\$\s*(\d{1,3}(?:\.\d{3})*,\d{2})";
            Match matchValor = Regex.Match(textoBruto, padraoValor);
            string valorEncontrado = matchValor.Success ? matchValor.Value : "Não encontrado";

            // Retorna o resultado completo e estruturado em JSON
            return Ok(new 
            { 
                Mensagem = "Leitura do comprovante concluída com sucesso!", 
                Cnpj = cnpjEncontrado,
                Data = dataEncontrada,
                ValorTotal = valorEncontrado,
                TextoCompleto = textoBruto 
            });
        }
        catch (Exception ex)
        {
            // Retorna erro 500 caso algo dê errado no processamento
            return StatusCode(500, $"Erro interno ao processar a imagem: {ex.Message}");
        }
    }
}