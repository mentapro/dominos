using MediatR;
namespace Dominos.Api.VouchersUploading;

internal class UploadVouchersCommand : IRequest
{
    public UploadVouchersCommand(Stream jsonFileStream)
    {
        JsonFileStream = jsonFileStream;
    }

    public Stream JsonFileStream { get; }
}