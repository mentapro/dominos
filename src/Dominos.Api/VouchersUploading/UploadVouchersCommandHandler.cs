using System.Text.Json;
using Dominos.Domain;
using Dominos.Persistence.Abstractions;
using Dominos.Persistence.Postgres;
using MediatR;
namespace Dominos.Api.VouchersUploading;

internal class UploadVouchersCommandHandler : IRequestHandler<UploadVouchersCommand>
{
    private record VoucherItem(Guid Id, string Name, decimal Price, string ProductCodes);

    private readonly IVoucherUploadRepository _repository;
    private readonly VouchersDbContext _context;

    public UploadVouchersCommandHandler(IVoucherUploadRepository repository, VouchersDbContext context) => (_repository, _context) = (repository, context);

    public async Task Handle(UploadVouchersCommand request, CancellationToken cancellation)
    {
        if (_context.Vouchers.Any())
        {
            return;
        }

        var stream = request.JsonFileStream;
        var vouchers = await JsonSerializer.DeserializeAsync<IReadOnlyCollection<VoucherItem>>(stream, (JsonSerializerOptions?)null, cancellation);
        if (vouchers is null)
        {
            throw new InvalidOperationException("Could not deserialize file with voucher data");
        }

        var dals = vouchers
                   .Select(x => new Voucher(
                       x.Id,
                       x.Name,
                       x.Price,
                       x.ProductCodes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList()))
                   .ToList();
        await _repository.InsertBatch(dals, cancellation);
    }
}