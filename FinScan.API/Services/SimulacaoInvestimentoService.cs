using System;
using System.Threading.Tasks;
using FinScan.API.DTOs;

namespace FinScan.API.Services
{
    public class SimulacaoInvestimentoService : ISimulacaoInvestimentoService
    {
        private readonly IIndicadoresFinanceirosService _indicadoresService;

        public SimulacaoInvestimentoService(IIndicadoresFinanceirosService indicadoresService)
        {
            _indicadoresService = indicadoresService;
        }

        public async Task<SimulacaoResponse> CalcularAsync(SimulacaoRequest request)
        {
            // 1. Obtém a taxa Selic atualizada via API do Bacen
            decimal selicAnual = await _indicadoresService.ObterTaxaSelicAtualAsync();

            // 2. Conversão da taxa anual para mensal 
            double taxaAnualDecimal = (double)(selicAnual / 100m);
            double taxaMensal = Math.Pow(1.0 + taxaAnualDecimal, 1.0 / 12.0) - 1.0;

            // 3. Variáveis de controle
            decimal valorTotalInvestido = request.ValorInicial + (request.AporteMensal * request.TempoMeses);
            double montanteAtual = (double)request.ValorInicial;

            // 4. Cálculo iterativo dos juros compostos + aportes
            for (int i = 0; i < request.TempoMeses; i++)
            {
                // Aplica o rendimento sobre o saldo do mês
                montanteAtual += montanteAtual * taxaMensal; 
                // Soma o aporte mensal no final do período
                montanteAtual += (double)request.AporteMensal; 
            }

            decimal valorTotalFinal = Math.Round((decimal)montanteAtual, 2);
            decimal rendimentoBruto = valorTotalFinal - valorTotalInvestido;

            return new SimulacaoResponse
            {
                ValorTotalInvestido = valorTotalInvestido,
                RendimentoBruto = rendimentoBruto,
                ValorTotalFinal = valorTotalFinal,
                TaxaSelicUtilizada = selicAnual
            };
        }
    }
}