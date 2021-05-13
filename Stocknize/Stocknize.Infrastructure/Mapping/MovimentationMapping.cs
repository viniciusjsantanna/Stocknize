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

            builder.Property(e => e.Quantity)
                .IsRequired();

            builder.Property(e => e.Price)
                .HasColumnType("money")
                .IsRequired();

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey("UserId");

            builder.Property(e => e.Type)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(e => e.CreatedAt);
        }
    }
}
