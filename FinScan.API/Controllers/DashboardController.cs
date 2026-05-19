using Microsoft.AspNetCore.Mvc;
using FinScan.API.Services;
using System.Linq;
using System.Collections.Generic; // Adicionado para reconhecer o List<>

namespace FinScan.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly CategorizadorService _categorizadorService;

    // A MÁGICA DA INJEÇÃO AQUI: 
    // O .NET lê esse parâmetro e injeta o serviço automaticamente!
    public DashboardController(CategorizadorService categorizadorService)
    {
        _categorizadorService = categorizadorService;
    }

    [HttpGet("summary")]
    public IActionResult GetCategorySummary()
    {
        var mockTransactions = new List<Transaction>
        {
            new ("Ifood", 50.00m),
            new ("Uber", 25.00m),
            new ("Farmacia", 100.00m),
            new ("Netflix", 40.00m)
        };

        var summary = mockTransactions
            .Select(t => new { 
                Categoria = _categorizadorService.Categorizar(t.Description), 
                Valor = t.Amount 
            })
            .GroupBy(x => x.Categoria)
            .Select(g => new { 
                Category = g.Key, 
                Total = g.Sum(x => x.Valor) 
            });

        return Ok(summary);
    }
}

public record Transaction(string Description, decimal Amount);