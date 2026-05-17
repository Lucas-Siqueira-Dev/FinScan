using System.Threading.Tasks;
using FinScan.API.DTOs;
using FinScan.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinScan.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulacaoController : ControllerBase
    {
        private readonly ISimulacaoInvestimentoService _simulacaoService;

        public SimulacaoController(ISimulacaoInvestimentoService simulacaoService)
        {
            _simulacaoService = simulacaoService;
        }

        [HttpPost]
        public async Task<IActionResult> SimularInvestimento([FromBody] SimulacaoRequest request)
        {
            // --- SISTEMA DE PERMISSÕES E LIMITES DE USO (FSVS-32) ---
            
            // Regra 1: Limite de tempo de simulação para conter abuso do servidor
            if (request.TempoMeses > 120)
            {
                return BadRequest(new { mensagem = "Limite de uso excedido: O tempo máximo permitido para simulação é de 120 meses (10 anos)." });
            }

            // Regra 2: Limite de valor de aporte para o plano estudantil/gratuito
            if (request.AporteMensal > 10000 || request.ValorInicial > 100000)
            {
                return BadRequest(new { mensagem = "Permissão negada: Valores acima de R$ 10.000 mensais ou R$ 100.000 iniciais exigem conta Premium." });
            }

            // Regra 3: Validação básica de valores negativos
            if (request.ValorInicial < 0 || request.AporteMensal < 0 || request.TempoMeses <= 0)
            {
                return BadRequest(new { mensagem = "Dados inválidos: Os valores informados devem ser maiores que zero." });
            }

            // ---------------------------------------------------------

            // Se passar pelos limites, executa o cálculo do serviço
            var resultado = await _simulacaoService.CalcularAsync(request);
            
            return Ok(resultado);
        }
    }
}