using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.CityRepository;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.ProgressNotifier.DI;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TypeMapping.DI;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.DI;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        return serviceCollection
            .AddConsoleProgressNotifier()
            
            .AddTypeMapping()
            
            .AddCityRepository(configurationRoot);
    }
}
