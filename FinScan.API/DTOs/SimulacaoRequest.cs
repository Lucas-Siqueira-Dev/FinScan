namespace FinScan.API.DTOs
{
    public class SimulacaoRequest
    {
        public decimal ValorInicial { get; set; }
        public decimal AporteMensal { get; set; }
        public int TempoMeses { get; set; }
    }
}