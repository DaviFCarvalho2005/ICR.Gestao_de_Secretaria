using ICR.Domain.Model;
using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.FederationAggregate;
using Microsoft.EntityFrameworkCore;

namespace ICR.Infra
{
    public class ConnectionContext : DbContext
    {
        // Construtor exigido pelo AddDbContext
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<Federation> Federations { get; set; }
        public DbSet<Church> Churches { get; set; }
        public DbSet<Cell> Cells { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Church>().OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.ZipCode).HasColumnName("ZipCode");
                address.Property(a => a.Street).HasColumnName("Street");
                address.Property(a => a.Number).HasColumnName("Number");
                address.Property(a => a.City).HasColumnName("City");
                address.Property(a => a.State).HasColumnName("State");
            });
        }
    }
}
