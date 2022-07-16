using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Domain;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DI;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.CityRepository;

public static class CityRepositoryServiceCollectionExtensions
{
    public static IServiceCollection AddCityRepository(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        return serviceCollection
            .AddTuiMusementHttpApi(configurationRoot)
            .AddScoped<ICityRepository, CityRepository>();
    }
}
