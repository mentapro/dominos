using MediatR;
namespace Dominos.Api.VouchersUploading;

internal class DataInitializationHostedService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DataInitializationHostedService(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    public async Task StartAsync(CancellationToken cancellation)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await using var fileStream = File.OpenRead("data.json");
        var command = new UploadVouchersCommand(fileStream);
        await mediator.Send(command, cancellation);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}