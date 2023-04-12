using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace Dominos.Api.VouchersUploading;

internal static class InternalVoucherEndpoints
{
    internal static void MapVoucherInternalEndpoints(this WebApplication app)
    {
        app.MapPost("api/internal/vouchers/upload", UploadVouchers).WithOpenApi();
    }

    private static async Task<Results<Ok, BadRequest<string>>> UploadVouchers(
        [FromServices] IMediator mediator,
        IFormFile file,
        CancellationToken cancellation)
    {
        await using var stream = file.OpenReadStream();
        var command = new UploadVouchersCommand(stream);
        try
        {
            await mediator.Send(command, cancellation);
        }
        catch (InvalidOperationException ex)
        {
            return TypedResults.BadRequest(ex.Message);
        }

        return TypedResults.Ok();
    }
}