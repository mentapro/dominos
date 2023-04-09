using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace Dominos.Persistence.Postgres;

public class VoucherContextFactory : IDesignTimeDbContextFactory<VouchersDbContext>
{
    public VouchersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<VouchersDbContext>();
        optionsBuilder.UseNpgsql("localhost");
        return new VouchersDbContext(optionsBuilder.Options);
    }
}