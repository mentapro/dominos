using Dominos.Domain;
namespace Dominos.Persistence.Abstractions.Queries;

public class VouchersQueryResponse
{
    public VouchersQueryResponse(IReadOnlyCollection<Voucher> vouchers, bool hasMoreItems)
    {
        Vouchers = vouchers ?? throw new ArgumentNullException(nameof(vouchers));
        HasMoreItems = hasMoreItems;
    }

    public IReadOnlyCollection<Voucher> Vouchers { get; }

    public bool HasMoreItems { get; }
}