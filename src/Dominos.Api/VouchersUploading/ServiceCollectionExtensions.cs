namespace Dominos.Api.VouchersUploading;

public static partial class DependencyInjection
{
    public static IServiceCollection AddDataInitialization(this IServiceCollection services)
        => services.AddHostedService<DataInitializationHostedService>();
}