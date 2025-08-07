using Serilog;

namespace GamingService.OutboxWorker.Extensions;

public static class DIExtensions
{
    public static HostApplicationBuilder UseSerilog(this HostApplicationBuilder builder, ConfigurationManager configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog(Log.Logger);
        return builder;
    }
}
