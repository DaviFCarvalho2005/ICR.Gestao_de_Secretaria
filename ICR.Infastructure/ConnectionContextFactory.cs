using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ICRManagement.Infra
{
    public class ConnectionContextFactory : IDesignTimeDbContextFactory<ConnectionContext>
    {
        public ConnectionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConnectionContext>();

            // mesma connection string que você tem no launchSettings.json da API
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=icr_connect;Username=icradmin;Password=root");

            return new ConnectionContext(optionsBuilder.Options);
        }
    }
}
