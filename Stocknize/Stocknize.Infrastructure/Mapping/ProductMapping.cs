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
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(e => e.Price)
                .IsRequired()
                .HasColumnType("money");

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey("ProductTypeId")
                .OnDelete(DeleteBehavior.NoAction); 

            builder.Property(e => e.CreatedAt);

            builder.HasOne(e => e.Company)
                .WithMany()
                .HasForeignKey("CompanyId")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
