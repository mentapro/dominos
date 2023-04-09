using AutoMapper;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;

namespace Dominos.Persistence.Postgres;

public class VoucherRepositoryMapping : Profile
{
    public VoucherRepositoryMapping()
    {
        CreateMap<VoucherDal, Voucher>()
                .ConstructUsing((dal, context) => new Voucher(
                    new VoucherId(dal.Id),
                    dal.Name,
                    dal.Price,
                    dal.ProductCodes))
                .IgnoreAllPropertiesWithAnInaccessibleSetter();
    }
}