using System.Net.Http.Json;

namespace FinScan.App.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5000/") };
    }

    public async Task<List<CategorySummary>> GetDashboardDataAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CategorySummary>>("api/dashboard/summary") ?? new List<CategorySummary>();
    }
}

public record CategorySummary(string Category, decimal Total);