using Dominio;
using Microsoft.EntityFrameworkCore;

namespace InfraEstrutura.BancoDeDados;
public class ApiDBContext : DbContext
{
    public DbSet<ContaTelegram> ContasTelegram { get; set; }
    public DbSet<LeadTelegram> LeadsTelegram { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=afonso;Username=afonso;Password=afonsoapi;Pooling=true;");
}
