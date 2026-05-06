using System.Text.RegularExpressions;

namespace FinScan.API.Services;

public static class ExtratorDados
{
    public static string ExtrairCnpj(string texto)
    {
        var match = Regex.Match(texto, @"\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}");
        return match.Success ? match.Value : "Não encontrado";
    }

    public static string ExtrairData(string texto)
    {
        var match = Regex.Match(texto, @"\d{2}/\d{2}/\d{4}");
        return match.Success ? match.Value : "Não encontrada";
    }

    public static string ExtrairValor(string texto)
    {
        var match = Regex.Match(texto, @"R\$\s*(\d{1,3}(?:\.\d{3})*,\d{2})");
        return match.Success ? match.Value : "Não encontrado";
    }
}