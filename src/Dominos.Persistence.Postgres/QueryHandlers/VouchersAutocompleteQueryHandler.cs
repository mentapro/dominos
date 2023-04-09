using AutoMapper;
using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Dominos.Persistence.Postgres.QueryHandlers;

public class VouchersAutocompleteQueryHandler : IRequestHandler<VouchersAutocompleteQuery, VouchersQueryResponse>
{
    private readonly VouchersDbContext _context;
    private readonly IMapper _mapper;

    public VouchersAutocompleteQueryHandler(VouchersDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

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
            results.Take(request.Limit).Select(_mapper.Map<Voucher>).ToList(),
            results.Count > request.Limit);
    }
}