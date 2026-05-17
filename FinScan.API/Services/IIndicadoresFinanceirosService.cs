using System.Threading.Tasks;

namespace FinScan.API.Services
{
    public interface IIndicadoresFinanceirosService
    {
        Task<decimal> ObterTaxaSelicAtualAsync();
    }
}