using Dominos.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dominos.Persistence.Postgres;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.ToTable("vouchers");

        builder.HasKey(x => x.Id);
        builder.Property(n => n.Id)
               .HasColumnName("id")
               .HasField("_id")
               .IsRequired()
               .ValueGeneratedNever();

        builder.Property(n => n.Name)
               .HasColumnName("name")
               .HasField("_name")
               .IsUnicode()
               .IsRequired();

        builder.Property(p => p.Price)
               .HasColumnName("price")
               .HasField("_price")
               .IsRequired();

        builder.Property(n => n.ProductCodes)
               .HasColumnName("product_codes")
               .HasField("_productCodes")
               .IsRequired();

        builder.HasIndex(x => x.Name, "IX_vouchers_name");
        // builder.HasIndex(x => x.Name, "IX_vouchers_name_gin").HasMethod("gin").HasOperators("gin_trgm_ops");
    }
}