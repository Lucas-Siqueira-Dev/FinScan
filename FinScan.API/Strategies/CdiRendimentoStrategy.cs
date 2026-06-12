using FinScan.API.DTOs;
using FinScan.API.Services;

namespace FinScan.API.Strategies;

public class CdiRendimentoStrategy : IRendimentoStrategy
{
    private readonly IIndicadoresFinanceirosService _indicadoresService;

    public CdiRendimentoStrategy(IIndicadoresFinanceirosService indicadoresService)
    {
        _indicadoresService = indicadoresService;
    }

    public async Task<SimulacaoResponse> CalcularProjecaoAsync(SimulacaoRequest request)
    {
        decimal selicAnual = await _indicadoresService.ObterTaxaSelicAtualAsync();
        double taxaMensal = Math.Pow(1.0 + (double)(selicAnual / 100m), 1.0 / 12.0) - 1.0;

        decimal valorTotalInvestido = request.ValorInicial + (request.AporteMensal * request.TempoMeses);
        double montanteAtual = (double)request.ValorInicial;

        for (int i = 0; i < request.TempoMeses; i++)
        {
            montanteAtual += montanteAtual * taxaMensal;
            montanteAtual += (double)request.AporteMensal;
        }

        return new SimulacaoResponse
        {
            ValorTotalInvestido = valorTotalInvestido,
            ValorTotalFinal = Math.Round((decimal)montanteAtual, 2),
            RendimentoBruto = Math.Round((decimal)montanteAtual, 2) - valorTotalInvestido,
            TaxaSelicUtilizada = selicAnual
        };
    }
}