using System.Text.Json;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.Endpoints;

public static class InternalVoucherEndpoints
{
    private record VoucherItem(Guid Id, string Name, decimal Price, string ProductCodes);

    internal static void MapVoucherInternalEndpoints(this WebApplication app)
    {
        app.MapPost("api/internal/vouchers/upload", UploadVouchers).WithOpenApi();
    }

    // injecting repository into endpoint is a bad practice.
    // here it is temporary solution because it is technical one-time endpoint
    private static async Task<Results<Ok, BadRequest<string>>> UploadVouchers(
        [FromServices] IVoucherUploadRepository repo,
        HttpContext context,
        IFormFile file,
        CancellationToken cancellation)
    {
        await using var stream = file.OpenReadStream();
        var vouchers = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<VoucherItem>>(stream, (JsonSerializerOptions?)null, cancellation);
        if (vouchers is null)
        {
            return TypedResults.BadRequest("Could not deserialize file with voucher data");
        }
        var dals = vouchers
                   .Select(x => new Voucher(
                       x.Id,
                       x.Name,
                       x.Price,
                       x.ProductCodes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()))
                   .ToList();
        await repo.InsertBatch(dals, cancellation);
        return TypedResults.Ok();
    }
}