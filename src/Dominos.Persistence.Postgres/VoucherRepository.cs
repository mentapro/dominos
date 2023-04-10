using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres;

internal class VoucherRepository : IVoucherRepository
{
    private readonly VouchersDbContext _context;

    public VoucherRepository(VouchersDbContext context) => _context = context;

    public Task<Voucher?> Get(Guid id, CancellationToken cancellation = default)
        => _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id, cancellation);
}