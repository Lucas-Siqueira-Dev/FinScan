using System.Threading.Tasks;
using FinScan.API.DTOs;

namespace FinScan.API.Services
{
    public interface ISimulacaoInvestimentoService
    {
        Task<SimulacaoResponse> CalcularAsync(SimulacaoRequest request);
    }
}