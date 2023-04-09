using MediatR;
namespace Dominos.Persistence.Abstractions.Queries;

public class VouchersAutocompleteQuery : IRequest<VouchersQueryResponse>
{
    public VouchersAutocompleteQuery(string nameSearch, int offset, int limit)
    {
        NameSearch = nameSearch ?? throw new ArgumentNullException(nameof(nameSearch));
        Offset = offset;
        Limit = limit;
    }

    public string NameSearch { get; }

    public int Offset { get; }

    public int Limit { get; }
}