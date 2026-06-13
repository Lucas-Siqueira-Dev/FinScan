using Microsoft.AspNetCore.Mvc;
using FinScan.API.Data;
using FinScan.API.Models;

namespace FinScan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceiptsController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ReceiptsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    public async Task<IActionResult> SalvarNota([FromBody] NovaNotaRequest request)
    {
        // Converte a data que vem do celular (string) para o formato UTC do Supabase
        DateTime dataEmissaoUtc = DateTime.TryParse(request.DataEmissao, out var dataParsed) 
            ? dataParsed.ToUniversalTime() 
            : DateTime.UtcNow;

        var novaTransacao = new ComprovanteFiscal
        {
            Id = Guid.NewGuid(), // Gera um ID único
            NomeEstabelecimento = request.Estabelecimento,
            ValorTotal = request.ValorTotal,
            DataEmissao = dataEmissaoUtc,
            
            // Preenchendo os dados NOT NULL exigidos pelo banco com dados provisórios
            CnpjEstabelecimento = "00.000.000/0000-00",
            ChaveAcesso = "GERADA-MANUALMENTE-SEM-SCAN",
            DataCaptura = DateTime.UtcNow
        };
        
        _dbContext.Comprovantes.Add(novaTransacao);
        await _dbContext.SaveChangesAsync();
        
        return Ok();
    }
}

public class NovaNotaRequest
{
    public string Estabelecimento { get; set; }
    public decimal ValorTotal { get; set; }
    public string DataEmissao { get; set; }
}