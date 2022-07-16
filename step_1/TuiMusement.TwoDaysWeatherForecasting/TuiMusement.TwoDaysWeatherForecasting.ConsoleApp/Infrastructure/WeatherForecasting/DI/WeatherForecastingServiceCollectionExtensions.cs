using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DI;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherForecasting.DI;

public static class WeatherForecastingServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherForecastingService(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        return serviceCollection
            .AddWeatherHttpApi(configurationRoot)
            .AddScoped<IWeatherForecastingService, WeatherForecastingService>();
    }
}
