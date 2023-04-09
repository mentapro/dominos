using Dominos.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Dominos.Persistence.Postgres;

public static partial class DependencyInjection
{
    public static IServiceCollection AddVouchersDatabase(this IServiceCollection services, DbConnectionOptions postgresOptions)
        => services
           .AddHostedService<DbMigrationHostedService>()
           .AddTransient<IVoucherRepository, VoucherRepository>()
           .AddDbContext<VouchersDbContext>(opt => opt.UseNpgsql(postgresOptions.ConnectionString));
}