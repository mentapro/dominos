using Dominos.Domain;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres;

public class VouchersDbContext : DbContext
{
    public VouchersDbContext(DbContextOptions<VouchersDbContext> opts) : base(opts)
    {
    }

    public DbSet<Voucher> Vouchers => Set<Voucher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VoucherConfiguration());
    }
}