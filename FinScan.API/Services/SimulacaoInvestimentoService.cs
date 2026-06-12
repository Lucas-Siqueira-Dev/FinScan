using FinScan.API.DTOs;
using FinScan.API.Strategies;

namespace FinScan.API.Services;

public class SimulacaoInvestimentoService : ISimulacaoInvestimentoService
{
    private readonly IRendimentoStrategy _estrategia;

    public SimulacaoInvestimentoService(IRendimentoStrategy estrategia)
    {
        _estrategia = estrategia;
    }

    public async Task<SimulacaoResponse> CalcularAsync(SimulacaoRequest request)
    {
        return await _estrategia.CalcularProjecaoAsync(request);
    }
}