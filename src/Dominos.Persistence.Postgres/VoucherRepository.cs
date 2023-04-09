using Dominos.Persistence.Abstractions;

namespace Dominos.Persistence.Postgres;

internal class VoucherRepository : IVoucherRepository
{
    private readonly VouchersDbContext _context;

    public VoucherRepository(VouchersDbContext context) => _context = context;

    public async Task InsertBatch(IReadOnlyCollection<VoucherDal> vouchers, CancellationToken cancellation = default)
    {
        await _context.AddRangeAsync(vouchers, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }
}