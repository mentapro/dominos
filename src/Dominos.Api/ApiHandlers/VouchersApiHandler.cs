using AutoMapper;
using Dominos.Api.Dtos;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.ApiHandlers;

public static class VouchersApiHandler
{
    public static async Task<IResult> GetVouchers(
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        VouchersRequestDto dto,
        CancellationToken cancellation)
    {
        var query = new VouchersQuery(dto.Name, dto.Offset, dto.Limit);
        var response = await mediator.Send(query, cancellation);
        return Results.Ok(mapper.Map<VoucherCollectionDto>(response));
    }

    public static async Task<IResult> GetVoucher(
        [FromServices] IVoucherRepository repository,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id,
        CancellationToken cancellation)
    {
        var voucherId = new VoucherId(id);
        var voucher = await repository.Get(voucherId, cancellation);
        return Results.Ok(mapper.Map<VoucherDto>(voucher));
    }
}