using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dominos.Persistence.Postgres.QueryHandlers;

public class VouchersQueryHandler : IRequestHandler<VouchersQuery, VouchersQueryResponse>
{
    private readonly VouchersDbContext _context;

    public VouchersQueryHandler(VouchersDbContext context) => _context = context;

    public async Task<VouchersQueryResponse> Handle(VouchersQuery request, CancellationToken cancellation)
    {
        var query = _context.Vouchers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(x => x.Name == request.Name);
        }

        query = query.OrderBy(x => x.Id);

        var results = await query
                            .Skip(request.Offset)
                            .Take(request.Limit + 1)
                            .ToListAsync(cancellation);

        return new VouchersQueryResponse(
            results.Take(request.Limit).ToList(),
            results.Count > request.Limit);
    }

    // NOTE: this code can be useful like an example of how we can optimize our requests to Database

    /*private static readonly Func<VouchersDbContext, int, int, IAsyncEnumerable<Voucher>> GetVouchersQuery =
            EF.CompileAsyncQuery((VouchersDbContext context, int offset, int limit) =>
                    context.Vouchers
                           .AsNoTracking()
                           .OrderBy(x => x.Id)
                           .Skip(offset)
                           .Take(limit));

    private static readonly Func<VouchersDbContext, string, int, int, IAsyncEnumerable<Voucher>> GetVouchersByNameQuery =
            EF.CompileAsyncQuery((VouchersDbContext context, string name, int offset, int limit) =>
                    context.Vouchers
                           .AsNoTracking()
                           .Where(x => x.Name == name)
                           .OrderBy(x => x.Id)
                           .Skip(offset)
                           .Take(limit));

    public async Task<VouchersQueryResponse> Handle(VouchersQuery request, CancellationToken cancellation)
    {
        var results = new List<Voucher>();
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            await foreach (var voucher in GetVouchersQuery(_context, request.Offset, request.Limit + 1))
            {
                results.Add(voucher);
            }
            return CreateVouchersQueryResponse(request, results);
        }
        else
        {
            await foreach (var voucher in GetVouchersByNameQuery(_context, request.Name, request.Offset, request.Limit + 1))
            {
                results.Add(voucher);
            }
            return CreateVouchersQueryResponse(request, results);
        }
    }

    private static VouchersQueryResponse CreateVouchersQueryResponse(VouchersQuery request, List<Voucher> results)
        => new VouchersQueryResponse(
            results.Take(request.Limit).ToList(),
            results.Count > request.Limit);*/

}