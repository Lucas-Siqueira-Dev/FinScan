using Microsoft.AspNetCore.Mvc;
using FinScan.API.Services;
using Microsoft.EntityFrameworkCore;
using FinScan.API.Data; 

namespace FinScan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ICategorizadorService _categorizadorService;
    private readonly AppDbContext _dbContext;

    public DashboardController(ICategorizadorService categorizadorService, AppDbContext dbContext)
    {
        _categorizadorService = categorizadorService;
        _dbContext = dbContext;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetCategorySummary()
    {
        var transacoesReais = await _dbContext.Comprovantes.ToListAsync();

        if (!transacoesReais.Any())
            return Ok(new List<object>()); 

        var summary = transacoesReais
            .Select(t => new { 
                // Agora puxando os nomes exatos do seu schema do Supabase
                Categoria = _categorizadorService.Categorizar(t.NomeEstabelecimento), 
                Valor = t.ValorTotal 
            })
            .GroupBy(x => x.Categoria)
            .Select(g => new { 
                Category = g.Key, 
                Total = g.Sum(x => x.Valor) 
            });

        return Ok(summary);
    }
}