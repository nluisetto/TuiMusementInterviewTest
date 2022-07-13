using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.ProgressNotifier.DI;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.DI;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddConsoleProgressNotifier();
    }
}
