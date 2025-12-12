using ICRManagement.Domain.Model.FederationAggregate;
using Microsoft.EntityFrameworkCore;

namespace ICRManagement.Infra
{
    public class ConnectionContext : DbContext
    {
        // Construtor exigido pelo AddDbContext
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<Federation> Federations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configurações adicionais de mapeamento, se necessário
        }
    }
}
