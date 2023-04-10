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
}