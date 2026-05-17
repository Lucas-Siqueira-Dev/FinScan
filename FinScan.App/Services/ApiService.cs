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
}

public record CategorySummary(string Category, decimal Total);