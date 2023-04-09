using System.Text.Json;
using Dominos.Persistence.Abstractions;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.ApiHandlers;

public static class UploadVouchersHandler
{
    private record VoucherItem(Guid Id, string Name, decimal Price, string ProductCodes);

    // injecting repository into endpoint is a bad practice.
    // here it is temporary solution because it is technical one-time endpoint
    public static async Task<IResult> UploadVouchers(
        [FromServices] IVoucherRepository repo,
        HttpContext context,
        IFormFile file,
        CancellationToken cancellation)
    {
        await using var stream = file.OpenReadStream();
        var vouchers = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<VoucherItem>>(stream, (JsonSerializerOptions?)null, cancellation);
        if (vouchers is null)
        {
            return Results.BadRequest("Could not deserialize file with voucher data");
        }
        var dals = vouchers.Select(x => new VoucherDal
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            ProductCodes = x.ProductCodes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries),
        }).ToList();
        await repo.InsertBatch(dals, cancellation);
        return Results.Ok();
    }
}