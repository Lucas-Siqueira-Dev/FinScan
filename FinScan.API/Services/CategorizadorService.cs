namespace FinScan.API.Services;

public class CategorizadorService
{
    // Task FSVS-40: Dicionário de palavras-chave base
    private readonly Dictionary<string, string[]> _categorias = new()
    {
        { "Alimentação", new[] { "ifood", "mercado", "pao", "restaurante", "mcdonalds", "burger", "confeitaria" } },
        { "Transporte", new[] { "uber", "99app", "posto", "gasolina", "combustivel", "shell", "ipiranga" } },
        { "Lazer", new[] { "cinema", "netflix", "spotify", "ingresso", "show", "pub", "bar" } },
        { "Saúde", new[] { "farmacia", "drogaria", "hospital", "clinica", "unimed" } }
    };
    
    // Task FSVS-41: Serviço que compara a descrição com o dicionário
    public string Categorizar(string textoExtraido)
    {
        if (string.IsNullOrWhiteSpace(textoExtraido)) return "Não Categorizado";

        string textoMinusculo = textoExtraido.ToLower();

        foreach (var categoria in _categorias)
        {
            foreach (var palavraChave in categoria.Value)
            {
                if (textoMinusculo.Contains(palavraChave))
                {
                    return categoria.Key;
                }
            }
        }
        // Task FSVS-42: Fluxo de exceção (Revisão Manual)
        return "Não Categorizado/Revisão Manual";
    }
}