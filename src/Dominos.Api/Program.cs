using Dominos.Api;
using Dominos.Api.ApiHandlers;
using Dominos.Api.Configuration;
using Dominos.Persistence.Postgres;
using MediatR;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

Log.Logger = new LoggerConfiguration().ConfigureBootstrapLogger().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    var settings = builder.Configuration.Get<VoucherAppSettings>()!;

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddAutoMapper(typeof(VouchersDbContext).Assembly, typeof(Program).Assembly);
    builder.Services.AddMediatR(typeof(MediatrPersistence).Assembly);

    builder.Services.AddVouchersDatabase(settings.PostgresOptions);

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

    var app = builder.Build();

    LogUnhandledExceptions(app);

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapPost("/api/vouchers/upload", UploadVouchersApiHandler.UploadVouchers);
    app.MapGet("api/vouchers", VouchersApiHandler.GetVouchers);
    app.MapGet("api/vouchers/{id:guid}", VouchersApiHandler.GetVoucher);

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