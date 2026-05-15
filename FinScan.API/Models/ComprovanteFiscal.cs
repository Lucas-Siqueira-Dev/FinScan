using System;
using System.ComponentModel.DataAnnotations;

namespace FinScan.API.Models
{
    public class ComprovanteFiscal
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CnpjEstabelecimento { get; set; } = string.Empty;
        public string NomeEstabelecimento { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public DateTime DataEmissao { get; set; }
        public string ChaveAcesso { get; set; } = string.Empty; // Os 44 dígitos
        public DateTime DataCaptura { get; set; } = DateTime.UtcNow;
    }
}