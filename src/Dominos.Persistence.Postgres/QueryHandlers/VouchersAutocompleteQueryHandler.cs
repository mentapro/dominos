using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class VouchersAutocompleteQueryHandler : IRequestHandler<VouchersAutocompleteQuery, VouchersQueryResponse>
{
    private readonly VouchersDbContext _context;
    private static readonly Func<VouchersDbContext, string, int, int, IAsyncEnumerable<Voucher>> AutocompleteQuery =
            EF.CompileAsyncQuery((VouchersDbContext context, string nameSearch, int offset, int limit) =>
                    context.Vouchers
                           .AsNoTracking()
                           .Where(x => x.Name.Contains(nameSearch))
                           .OrderBy(x => x.Id)
                           .Skip(offset)
                           .Take(limit));

    public VouchersAutocompleteQueryHandler(VouchersDbContext context) => _context = context;

    public async Task<VouchersQueryResponse> Handle(VouchersAutocompleteQuery request, CancellationToken cancellation)
    {
        var results = new List<Voucher>();
        await foreach (var voucher in AutocompleteQuery(_context, request.NameSearch, request.Offset, request.Limit + 1))
        {
            results.Add(voucher);
        }
        return new VouchersQueryResponse(
            results.Take(request.Limit).ToList(),
            results.Count > request.Limit);
    }
}