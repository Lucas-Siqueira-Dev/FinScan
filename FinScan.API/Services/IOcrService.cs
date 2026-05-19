using System.Threading.Tasks;

namespace FinScan.API.Services
{
    public interface IOcrService
    {
        Task<string> ExtrairTextoDaImagemAsync(string caminhoImagem);
    }
}