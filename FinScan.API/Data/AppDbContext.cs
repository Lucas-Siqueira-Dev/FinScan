using FinScan.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FinScan.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Esta propriedade representa a tabela no banco de dados
        public DbSet<ComprovanteFiscal> Comprovantes { get; set; }
    }
}