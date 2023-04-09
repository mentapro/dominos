namespace Dominos.Persistence.Abstractions
{
    public interface IVoucherUploadRepository
    {
        Task InsertBatch(IReadOnlyCollection<VoucherDal> vouchers, CancellationToken cancellation = default);
    }
}