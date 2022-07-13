using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Common;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.ProgressNotifier.DI;

public static class ConsoleProgressNotifierServiceCollectionExtensions
{
    public static IServiceCollection AddConsoleProgressNotifier(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<IProgressNotifier, ConsoleProgressNotifier>();
    }
}
