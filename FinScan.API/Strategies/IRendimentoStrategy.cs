using FinScan.API.DTOs;

namespace FinScan.API.Strategies;

public interface IRendimentoStrategy
{
    Task<SimulacaoResponse> CalcularProjecaoAsync(SimulacaoRequest request);
}