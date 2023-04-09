namespace Dominos.Persistence.Abstractions;

public interface IVoucherRepository
{
    Task InsertBatch(IReadOnlyCollection<VoucherDal> vouchers, CancellationToken cancellation = default);
}