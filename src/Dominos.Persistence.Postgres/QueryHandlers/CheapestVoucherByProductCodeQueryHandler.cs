using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class CheapestVoucherByProductCodeQueryHandler : IRequestHandler<CheapestVoucherByProductCodeQuery, Voucher?>
{
    private readonly VouchersDbContext _context;

    public CheapestVoucherByProductCodeQueryHandler(VouchersDbContext context) => _context = context;

    public Task<Voucher?> Handle(CheapestVoucherByProductCodeQuery request, CancellationToken cancellation)
        => _context.Vouchers
                   .AsNoTracking()
                   .Where(x => x.ProductCodes.Contains(request.ProductCode))
                   .OrderBy(x => x.Price)
                   .ThenBy(x => x.Id)
                   .FirstOrDefaultAsync(cancellation);
}