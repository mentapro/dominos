using Dominos.Domain;
namespace Dominos.Persistence.Abstractions;

public interface IVoucherRepository
{
    Task<Voucher?> Get(VoucherId id, CancellationToken cancellation = default);
}