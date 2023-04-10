using Dominos.Api;
using Dominos.Api.Endpoints;
using Dominos.Persistence.Postgres;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using ILogger = Microsoft.Extensions.Logging.ILogger;

Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
             .MinimumLevel.Override("System", LogEventLevel.Warning)
             .Enrich.FromLogContext()
             .WriteTo.Console(new RenderedCompactJsonFormatter())
             .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var settings = builder.Configuration.Get<VoucherAppSettings>()!;

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining<MediatrPersistence>());

    builder.Services.AddVouchersDatabase(settings.PostgresOptions);

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    LogUnhandledExceptions(app);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapGroup("/api/vouchers")
       .MapVoucherEndpoints()
       .WithTags("Voucher Endpoints");
    app.MapVoucherInternalEndpoints();

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

static void LogUnhandledExceptions(IHost host) => AppDomain.CurrentDomain.UnhandledException +=
        (sender, args) => host.Services.GetRequiredService<ILogger>().LogCritical((Exception)args.ExceptionObject, "Unhandled exception");

public partial class Program
{
}