using Dominos.Api.Endpoints.Dtos;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Dominos.Persistence.Abstractions.Queries;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Endpoints;

public static class VoucherEndpoints
{
    public static RouteGroupBuilder MapVoucherEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("/", GetVouchers).WithOpenApi();
        group.MapGet("/{voucherId:guid}", GetVoucher).WithOpenApi();
        group.MapGet("/cheapest-by-product", GetCheapestVoucherByProductCode).WithOpenApi();
        group.MapGet("/autocomplete", AutocompleteByName).WithOpenApi();
        return group;
    }

    public static async Task<Ok<VoucherCollectionDto>> GetVouchers(
        [FromServices] IMediator mediator,
        [AsParameters] VouchersRequestDto dto,
        CancellationToken cancellation)
    {
        var query = new VouchersQuery(dto.Name, dto.Offset, dto.Limit);
        var response = await mediator.Send(query, cancellation);
        return TypedResults.Ok(new VoucherCollectionDto
        {
            Items = response.Vouchers,
            HasMoreItems = response.HasMoreItems,
        });
    }

    public static async Task<Results<Ok<Voucher>, NotFound>> GetVoucher(
        [FromServices] IVoucherRepository repository,
        [FromRoute] Guid voucherId,
        CancellationToken cancellation)
    {
        var voucher = await repository.Get(voucherId, cancellation);
        if (voucher is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(voucher);
    }

    public static async Task<Results<Ok<Voucher>, NotFound>> GetCheapestVoucherByProductCode(
        [FromServices] IMediator mediator,
        [FromQuery(Name = "product_code")] string productCode,
        CancellationToken cancellation)
    {
        var query = new CheapestVoucherByProductCodeQuery(productCode);
        var voucher = await mediator.Send(query, cancellation);
        if (voucher is null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(voucher);
    }

    public static async Task<Ok<VoucherCollectionDto>> AutocompleteByName(
        [FromServices] IMediator mediator,
        [AsParameters] VouchersAutocompleteRequestDto dto,
        CancellationToken cancellation)
    {
        var query = new VouchersAutocompleteQuery(dto.NameSearch, dto.Offset, dto.Limit);
        var response = await mediator.Send(query, cancellation);
        return TypedResults.Ok(new VoucherCollectionDto
        {
            Items = response.Vouchers,
            HasMoreItems = response.HasMoreItems,
        });
    }
}