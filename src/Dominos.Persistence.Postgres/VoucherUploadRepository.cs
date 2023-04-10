using Dominos.Domain;
using Dominos.Persistence.Abstractions;

namespace Dominos.Persistence.Postgres;

internal class VoucherUploadRepository : IVoucherUploadRepository
{
    private readonly VouchersDbContext _context;

    public VoucherUploadRepository(VouchersDbContext context) => _context = context;

    public async Task InsertBatch(IReadOnlyCollection<Voucher> vouchers, CancellationToken cancellation = default)
    {
        await _context.AddRangeAsync(vouchers, cancellation);
        await _context.SaveChangesAsync(cancellation);
    }
}