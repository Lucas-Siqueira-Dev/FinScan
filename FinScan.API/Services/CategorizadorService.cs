using Microsoft.Extensions.Configuration;
using System.Linq;

namespace FinScan.API.Services
{
    public interface ICategorizadorService
    {
        string Categorizar(string textoComprovante);
    }

    public class CategorizadorService : ICategorizadorService
    {
        private readonly IConfiguration _configuration;

        // Injetando o IConfiguration (DIP)
        public CategorizadorService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Categorizar(string textoComprovante)
        {
            if (string.IsNullOrWhiteSpace(textoComprovante))
                return "Outros";

            textoComprovante = textoComprovante.ToLower();

            // Busca as categorias direto do appsettings.json
            var categoriasConfig = _configuration.GetSection("CategoriasPalavrasChave").GetChildren();

            foreach (var categoria in categoriasConfig)
            {
                var nomeCategoria = categoria.Key;
                var palavrasChave = categoria.Value?.Split(',') ?? Array.Empty<string>();

                if (palavrasChave.Any(palavra => textoComprovante.Contains(palavra.Trim().ToLower())))
                {
                    return nomeCategoria;
                }
            }

            return "Outros";
        }
    }
}