using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dominos.Persistence.Postgres;

public class VoucherConfiguration : IEntityTypeConfiguration<VoucherDal>
{
    public void Configure(EntityTypeBuilder<VoucherDal> builder)
    {
        builder.ToTable("vouchers");

        builder.HasKey(x => x.Id);
        builder.Property(n => n.Id)
               .HasColumnName("id")
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(n => n.Name)
               .HasColumnName("name")
               .IsUnicode()
               .IsRequired();

        builder.Property(p => p.Price)
               .HasColumnName("price")
               .IsRequired();

        builder.Property(n => n.ProductCodes)
               .HasColumnName("product_codes")
               .IsRequired();
    }
}