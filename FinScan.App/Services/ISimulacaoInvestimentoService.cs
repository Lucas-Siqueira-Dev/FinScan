namespace FinScan.App.Services;

// 1. O App tem os seus próprios DTOs para receber o JSON da API
public class SimulacaoRequest
{
    public decimal ValorInicial { get; set; }
    public decimal AporteMensal { get; set; }
    public int TempoMeses { get; set; }
}

public class SimulacaoResponse
{
    public decimal ValorTotalInvestido { get; set; }
    public decimal RendimentoBruto { get; set; }
    public decimal ValorTotalFinal { get; set; }
    public decimal TaxaSelicUtilizada { get; set; }
}

// 2. O contrato que a ViewModel espera
public interface ISimulacaoInvestimentoService
{
    Task<SimulacaoResponse> CalcularAsync(SimulacaoRequest request);
}

// 3. O serviço "Fake" para testarmos a UI hoje
public class MockSimulacaoService : ISimulacaoInvestimentoService
{
    public async Task<SimulacaoResponse> CalcularAsync(SimulacaoRequest request)
    {
        // Simula o delay da rede (loading do botão)
        await Task.Delay(1500); 

        decimal investido = request.ValorInicial + (request.AporteMensal * request.TempoMeses);
        
        return new SimulacaoResponse
        {
            ValorTotalInvestido = investido,
            RendimentoBruto = investido * 0.20m, // 20% de lucro fake
            ValorTotalFinal = investido * 1.20m,
            TaxaSelicUtilizada = 10.5m
        };
    }
}