using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace Dominos.Api;

public static partial class DependencyInjection
{
    public static LoggerConfiguration ConfigureBootstrapLogger(this LoggerConfiguration configuration) =>
            configuration
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console(new RenderedCompactJsonFormatter());
}