using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class VouchersAutocompleteQueryHandler : IRequestHandler<VouchersAutocompleteQuery, VouchersQueryResponse>
{
    private readonly VouchersDbContext _context;

    public VouchersAutocompleteQueryHandler(VouchersDbContext context) => _context = context;

    public async Task<VouchersQueryResponse> Handle(VouchersAutocompleteQuery request, CancellationToken cancellation)
    {
        var results = await _context.Vouchers
                                    .AsNoTracking()
                                    .Where(x => x.Name.Contains(request.NameSearch))
                                    .OrderBy(x => x.Id)
                                    .Skip(request.Offset)
                                    .Take(request.Limit + 1)
                                    .ToListAsync(cancellation);

        return new VouchersQueryResponse(
            results.Take(request.Limit).ToList(),
            results.Count > request.Limit);
    }
}