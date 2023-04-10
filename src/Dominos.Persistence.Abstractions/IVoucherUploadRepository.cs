using Dominos.Domain;
namespace Dominos.Persistence.Abstractions
{
    public interface IVoucherUploadRepository
    {
        Task InsertBatch(IReadOnlyCollection<Voucher> vouchers, CancellationToken cancellation = default);
    }
}