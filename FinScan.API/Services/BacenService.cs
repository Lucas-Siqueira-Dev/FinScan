using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FinScan.API.DTOs;

namespace FinScan.API.Services
{
    public class BacenService : IIndicadoresFinanceirosService
    {
        private readonly HttpClient _httpClient;

        public BacenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // Endpoint base do Banco Central
            _httpClient.BaseAddress = new Uri("https://api.bcb.gov.br/");
        }

        public async Task<decimal> ObterTaxaSelicAtualAsync()
        {
            try
            {
                // Código 432 = Taxa Selic Meta
                // Pegamos apenas o último registro (ultimos/1)
                var response = await _httpClient.GetFromJsonAsync<List<BacenData>>("dados/serie/bcdata.sgs.432/dados/ultimos/1?formato=json");
                
                if (response != null && response.Count > 0)
                {
                    // O BCB retorna o valor como string com ponto (ex: "10.50")
                    if (decimal.TryParse(response[0].Valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out var taxa))
                    {
                        return taxa;
                    }
                }
                
                // Fallback de segurança
                return 10.50m; 
            }
            catch
            {
                // Em caso de falha de rede, retornamos um fallback realista para não quebrar a simulação do usuário (e a apresentação)
                return 10.50m;
            }
        }
    }
}