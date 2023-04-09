using AutoMapper;
using Dominos.Api.Dtos;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Endpoints;

public static class VoucherEndpoints
{
    internal static void MapVoucherEndpoints(this WebApplication app)
    {
        app.MapGet("api/vouchers", GetVouchers).WithOpenApi();
        app.MapGet("api/vouchers/{id:guid}", GetVoucher).WithOpenApi();
        app.MapGet("api/vouchers/cheapest-by-product", GetCheapestVoucherByProductCode).WithOpenApi();
        app.MapGet("api/vouchers/autocomplete", AutocompleteByName).WithOpenApi();
    }

    private static async Task<Ok<VoucherCollectionDto>> GetVouchers(
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        [AsParameters] VouchersRequestDto dto,
        CancellationToken cancellation)
    {
        var query = new VouchersQuery(dto.Name, dto.Offset, dto.Limit);
        var response = await mediator.Send(query, cancellation);
        return TypedResults.Ok(mapper.Map<VoucherCollectionDto>(response));
    }

    private static async Task<Results<Ok<VoucherDto>, NotFound>> GetVoucher(
        [FromServices] IVoucherRepository repository,
        [FromServices] IMapper mapper,
        [FromRoute] Guid id,
        CancellationToken cancellation)
    {
        var voucherId = new VoucherId(id);
        var voucher = await repository.Get(voucherId, cancellation);
        if (voucher is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<VoucherDto>(voucher));
    }

    private static async Task<Results<Ok<VoucherDto>, NotFound>> GetCheapestVoucherByProductCode(
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        [FromQuery(Name = "product_code")] string productCode,
        CancellationToken cancellation)
    {
        var query = new CheapestVoucherByProductCodeQuery(productCode);
        var voucher = await mediator.Send(query, cancellation);
        if (voucher is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(mapper.Map<VoucherDto>(voucher));
    }

    private static async Task<Ok<VoucherCollectionDto>> AutocompleteByName(
        [FromServices] IMediator mediator,
        [FromServices] IMapper mapper,
        [AsParameters] VouchersAutocompleteRequestDto dto,
        CancellationToken cancellation)
    {
        var query = new VouchersAutocompleteQuery(dto.NameSearch, dto.Offset, dto.Limit);
        var response = await mediator.Send(query, cancellation);
        return TypedResults.Ok(mapper.Map<VoucherCollectionDto>(response));
    }
}