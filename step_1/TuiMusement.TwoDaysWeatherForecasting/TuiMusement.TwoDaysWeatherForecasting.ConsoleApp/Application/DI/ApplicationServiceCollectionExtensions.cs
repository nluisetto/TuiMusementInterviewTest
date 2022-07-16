using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.Features.TwoDaysWeatherForecasting.DI;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Application.DI;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationFeatures(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTwoDaysWeatherForecastingFeature();
    }
}
