using Microsoft.EntityFrameworkCore;
using Stocknize.Domain.Entities;

namespace Stocknize.Infrastructure.Context
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        //public DbSet<Product> Products { get; }
        //public DbSet<Inventory> Inventories { get; }
        //public DbSet<Movimentation> Movimentations { get; }
        //public DbSet<User> Users { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFContext).Assembly);
        }
    }
}
