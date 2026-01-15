using ICR.Domain.Model.CellAggregate;
using ICR.Domain.Model.ChurchAggregate;
using ICR.Domain.Model.FederationAggregate;
using ICR.Domain.Model.FamilyAggregate;
using ICR.Domain.Model.MemberAggregate;
using ICR.Domain.Model.MinisterAggregate;
using ICR.Domain.Model.UserRoleAgreggate;

using Microsoft.EntityFrameworkCore;
using ICR.Domain.Model.RepassAggregate;

namespace ICR.Infra
{
    public class ConnectionContext : DbContext
    {
        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }

        public DbSet<Federation> Federations => Set<Federation>();
        public DbSet<Church> Churches => Set<Church>();
        public DbSet<Cell> Cells => Set<Cell>();
        public DbSet<Family> Families => Set<Family>();
        public DbSet<Member> Members => Set<Member>();
        public DbSet<Minister> Ministers => Set<Minister>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Repass> Repasses => Set<Repass>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Church -> Address (Value Object)
            modelBuilder.Entity<Church>().OwnsOne(c => c.Address, address =>
            {
                address.Property(a => a.ZipCode).HasColumnName("ZipCode");
                address.Property(a => a.Street).HasColumnName("Street");
                address.Property(a => a.Number).HasColumnName("Number");
                address.Property(a => a.City).HasColumnName("City");
                address.Property(a => a.State).HasColumnName("State");
            });

            // Minister -> Address (Value Object)
            modelBuilder.Entity<Minister>().OwnsOne(m => m.Address, address =>
            {
                address.Property(a => a.ZipCode).HasColumnName("ZipCode");
                address.Property(a => a.Street).HasColumnName("Street");
                address.Property(a => a.Number).HasColumnName("Number");
                address.Property(a => a.City).HasColumnName("City");
                address.Property(a => a.State).HasColumnName("State");
            });

            // UserRole chave composta (isso aqui SEMPRE esquecem)
            modelBuilder.Entity<UserRole>()
        }
    }
}
