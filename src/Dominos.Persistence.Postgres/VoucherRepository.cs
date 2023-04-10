using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres;

internal class VoucherRepository : IVoucherRepository
{
    private readonly VouchersDbContext _context;
    private static readonly Func<VouchersDbContext, Guid, Task<Voucher?>> GetVoucherAsync =
            EF.CompileAsyncQuery((VouchersDbContext context, Guid id) =>
                    context.Vouchers.FirstOrDefault(x => x.Id == id));

    public VoucherRepository(VouchersDbContext context) => _context = context;

    public Task<Voucher?> Get(Guid id, CancellationToken cancellation = default)
        => GetVoucherAsync(_context, id);
}