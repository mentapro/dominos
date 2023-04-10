using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class CheapestVoucherByProductCodeQueryHandler : IRequestHandler<CheapestVoucherByProductCodeQuery, Voucher?>
{
    private readonly VouchersDbContext _context;
    private static readonly Func<VouchersDbContext, string, Task<Voucher?>> GetCheapestVoucherByProductCodeQuery =
            EF.CompileAsyncQuery((VouchersDbContext context, string productCode) =>
                    context.Vouchers
                           .AsNoTracking()
                           .Where(x => x.ProductCodes.Contains(productCode))
                           .OrderBy(x => x.Price)
                           .ThenBy(x => x.Id)
                           .FirstOrDefault());

    public CheapestVoucherByProductCodeQueryHandler(VouchersDbContext context) => _context = context;

    public Task<Voucher?> Handle(CheapestVoucherByProductCodeQuery request, CancellationToken cancellation)
        => GetCheapestVoucherByProductCodeQuery(_context, request.ProductCode);
}