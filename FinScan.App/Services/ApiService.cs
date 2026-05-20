using System.Net.Http.Json;
using FinScan.App.Models;

namespace FinScan.App.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("https://finscan-api.onrender.com/") };
    }

    public async Task<List<CategorySummary>> GetDashboardDataAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategorySummary>>("api/dashboard/summary") ?? new List<CategorySummary>();
    }
    
    public async Task<SimulacaoResponse?> CalcularRentabilidadeAsync(SimulacaoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/simulacao/calcular", request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<SimulacaoResponse>();
        }
        
        return null; 
    }

    // --- NOVA FEATURE: SALVAR NOTA FISCAL (OCR) ---
    public async Task<bool> SalvarNotaFiscalAsync(string nome, decimal valor, string data)
    {
        try
        {
            // Monta o pacote de dados (JSON)
            var payload = new
            {
                Estabelecimento = nome,
                ValorTotal = valor,
                DataEmissao = data
            };

            // Dispara um POST para a rota combinando BaseAddress + "api/receipts"
            var resposta = await _httpClient.PostAsJsonAsync("api/receipts", payload);

            return resposta.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar na API web: {ex.Message}");
            return false;
        }
    }
}

public record CategorySummary(string Category, decimal Total);