using Dominos.Domain;
using MediatR;
namespace Dominos.Persistence.Abstractions.Queries;

public class CheapestVoucherByProductCodeQuery : IRequest<Voucher?>
{
    public CheapestVoucherByProductCodeQuery(string productCode) => ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));

    public string ProductCode { get; }
}