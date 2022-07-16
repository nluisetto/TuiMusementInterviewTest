using Microsoft.Extensions.DependencyInjection;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Features.TwoDaysWeatherForecasting.DI;

public static class TwoDaysWeatherForecastingServiceCollectionExtensions
{
    public static IServiceCollection AddTwoDaysWeatherForecastingFeature(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ITwoDaysWeatherForecastingService, TwoDaysWeatherForecastingService>();
    }
}
