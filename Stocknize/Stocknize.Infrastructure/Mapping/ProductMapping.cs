using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocknize.Domain.Entities;

namespace Stocknize.Infrastructure.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Description)
                .HasColumnType("varchar(300)");

            builder.Property(e => e.Price)
                .HasColumnType("money");

            builder.Property(e => e.CreatedAt);
        }
    }
}
