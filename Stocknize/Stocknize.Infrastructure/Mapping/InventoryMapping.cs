using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocknize.Domain.Entities;

namespace Stocknize.Infrastructure.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.HasOne(e => e.Product)
                .WithOne()
                .HasForeignKey<Inventory>("ProductId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(e => e.CreatedAt);
        }
    }
}
