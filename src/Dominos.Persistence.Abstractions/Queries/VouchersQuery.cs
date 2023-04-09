using MediatR;
namespace Dominos.Persistence.Abstractions.Queries;

public class VouchersQuery : IRequest<VouchersQueryResponse>
{
    public VouchersQuery(string? name, int offset, int limit)
    {
        Name = name;
        Offset = offset;
        Limit = limit;
    }

    public string? Name { get; }

    public int Offset { get; }

    public int Limit { get; }
}