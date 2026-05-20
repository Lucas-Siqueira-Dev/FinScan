using System.Text.RegularExpressions;

namespace FinScan.App.Services;

public class ReceiptAnalyzer
{
    // Retornamos uma "Tupla" para devolver os 3 dados de uma vez só de forma elegante
    public static (string Nome, decimal Valor, string Data) Analisar(string textoBruto)
    {
        if (string.IsNullOrWhiteSpace(textoBruto))
            return ("Erro na leitura", 0.00m, DateTime.Now.ToString("dd/MM/yyyy"));

        // 1. Data (Procura pelo padrão brasileiro dd/mm/yyyy)
        var matchData = Regex.Match(textoBruto, @"\d{2}/\d{2}/\d{4}");
        string data = matchData.Success ? matchData.Value : DateTime.Now.ToString("dd/MM/yyyy");

        // 2. Valor (Procura números no formato de moeda e pega o MAIOR deles, que costuma ser o Total)
        var matchesValores = Regex.Matches(textoBruto, @"\b\d+[.,]\d{2}\b");
        decimal maiorValor = 0;
        
        foreach (Match match in matchesValores)
        {
            // O OCR pode ler ponto ou vírgula, normalizamos para tentar converter
            if (decimal.TryParse(match.Value.Replace(".", ","), out decimal valorEncontrado))
            {
                if (valorEncontrado > maiorValor) 
                    maiorValor = valorEncontrado;
            }
        }

        // 3. Estabelecimento (Geralmente a primeira linha de texto do cupom fiscal)
        var linhas = textoBruto.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        string nome = linhas.Length > 0 ? linhas[0].Trim() : "Estabelecimento não identificado";

        // Limita o tamanho do nome caso o OCR junte várias palavras
        if (nome.Length > 30) nome = nome.Substring(0, 30);

        return (nome, maiorValor, data);
    }
}