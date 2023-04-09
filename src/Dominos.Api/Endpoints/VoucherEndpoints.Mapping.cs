using AutoMapper;
using Dominos.Api.Dtos;
using Dominos.Domain;
using Dominos.Persistence.Abstractions.Queries;
namespace Dominos.Api.Endpoints;

public class VoucherEndpointsMappings : Profile
{
    public VoucherEndpointsMappings()
    {
        CreateMap<Voucher, VoucherDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Id));

        CreateMap<VouchersQueryResponse, VoucherCollectionDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Vouchers));
    }
}