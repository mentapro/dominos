using AutoMapper;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres;

internal class VoucherRepository : IVoucherRepository
{
    private readonly VouchersDbContext _context;
    private readonly IMapper _mapper;

    public VoucherRepository(VouchersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Voucher?> Get(VoucherId id, CancellationToken cancellation = default)
    {
        var voucherDal = await _context.Vouchers.FirstOrDefaultAsync(x => x.Id == id.Id, cancellation);
        return voucherDal != null ? _mapper.Map<Voucher>(voucherDal) : null;
    }
}