using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Dominos.Persistence.Postgres;

public static partial class DependencyInjection
{
    public static IServiceCollection AddVouchersDatabase(this IServiceCollection services, DbConnectionOptions postgresOptions)
        => services
           .AddHostedService<DbMigrationHostedService>()
           .AddScoped<IVoucherRepository, VoucherRepository>()
           .AddScoped<IVoucherUploadRepository, VoucherUploadRepository>()
           .AddDbContext<VouchersDbContext>(opt => opt.UseNpgsql(postgresOptions.ConnectionString));
}