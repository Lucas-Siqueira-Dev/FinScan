namespace FinScan.API.DTOs
{
    public class SimulacaoResponse
    {
        public decimal ValorTotalInvestido { get; set; }
        public decimal RendimentoBruto { get; set; }
        public decimal ValorTotalFinal { get; set; }
        public decimal TaxaSelicUtilizada { get; set; }
    }
}