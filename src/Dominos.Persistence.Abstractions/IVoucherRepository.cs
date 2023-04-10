using Dominos.Domain;
namespace Dominos.Persistence.Abstractions;

public interface IVoucherRepository
{
    Task<Voucher?> Get(Guid id, CancellationToken cancellation = default);
}