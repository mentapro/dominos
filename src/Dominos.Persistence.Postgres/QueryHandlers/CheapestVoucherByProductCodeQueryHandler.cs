using AutoMapper;
using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class CheapestVoucherByProductCodeQueryHandler : IRequestHandler<CheapestVoucherByProductCodeQuery, Voucher?>
{
    private readonly VouchersDbContext _context;
    private readonly IMapper _mapper;

    public CheapestVoucherByProductCodeQueryHandler(VouchersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Voucher?> Handle(CheapestVoucherByProductCodeQuery request, CancellationToken cancellation)
    {
        var voucherDal = await _context.Vouchers
                                       .AsNoTracking()
                                       .Where(x => x.ProductCodes.Contains(request.ProductCode))
                                       .OrderBy(x => x.Price)
                                       .ThenBy(x => x.Id)
                                       .FirstOrDefaultAsync(cancellation);
        return voucherDal is not null ? _mapper.Map<Voucher>(voucherDal) : null;
    }
}