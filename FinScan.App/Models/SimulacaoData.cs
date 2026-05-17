namespace FinScan.App.Models;

public class SimulacaoRequest
{
    public decimal ValorInicial { get; set; }
    public decimal AporteMensal { get; set; }
    public int TempoMeses { get; set; }
}

public class SimulacaoResponse
{
    public decimal ValorTotal { get; set; }
}