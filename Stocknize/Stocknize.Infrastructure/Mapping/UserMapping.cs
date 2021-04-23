using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stocknize.Domain.Entities;

namespace Stocknize.Infrastructure.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.Name)
                .HasColumnType("varchar(100)");

            builder.Property(e => e.Cpf)
                .HasColumnType("varchar(11)");

            builder.OwnsOne(
                e => e.Credentials,
                e =>
                {
                    e.ToTable("Credentials");
                    e.Property(e => e.Login)
                        .HasColumnType("varchar(50)");
                    e.Property(e => e.Password)
                        .HasColumnType("varchar(max)");
                    e.Property(e => e.Salt)
                        .HasColumnType("varchar(max)");
                });
        }
    }
}
