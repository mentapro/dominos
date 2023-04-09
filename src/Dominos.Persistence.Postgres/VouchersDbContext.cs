using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres;

public class VouchersDbContext : DbContext
{
    public VouchersDbContext(DbContextOptions<VouchersDbContext> opts)
            : base(opts)
    {
    }

    public DbSet<VoucherDal> Vouchers => Set<VoucherDal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new VoucherConfiguration());
    }
}