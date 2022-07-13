using Microsoft.Extensions.Hosting;
using Serilog;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Logging;

public static class LoggingServiceCollectionExtensions
{
    public static IHostBuilder ConfigureLogging(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseSerilog((hostBuilderContext, loggerConfiguration) =>
            {
                loggerConfiguration
                    .WriteTo.Console()
                    .MinimumLevel.Warning()
                    .ReadFrom
                    .Configuration(hostBuilderContext.Configuration);
            });
    }
}
