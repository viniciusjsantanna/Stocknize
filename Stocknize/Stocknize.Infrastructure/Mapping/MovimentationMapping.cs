using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocknize.Domain.Entities;

namespace Stocknize.Infrastructure.Mapping
{
    public class MovimentationMapping : IEntityTypeConfiguration<Movimentation>
    {
        public void Configure(EntityTypeBuilder<Movimentation> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.Property(e => e.Quantity);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(e => e.Type)
                .HasConversion<string>();

            builder.Property(e => e.CreatedAt);
        }
    }
}
