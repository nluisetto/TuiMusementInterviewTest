using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Configuration;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.Configuration;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DI;

public static class WeatherApiServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherHttpApi(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
    {
        serviceCollection
            .Configure<WeatherApiConfiguration>(configuration.GetSection(WeatherApiConfiguration.ConfigurationSectionName));
        
        serviceCollection
            .AddHttpClient(WeatherApiClient.HttpClientName, httpClient =>
            {
                var weatherApiConfiguration = configuration.GetSection(WeatherApiConfiguration.ConfigurationSectionName).Get<WeatherApiConfiguration>();

                if (string.IsNullOrWhiteSpace(weatherApiConfiguration.BaseUrl))
                    throw new ConfigurationException($"{WeatherApiConfiguration.ConfigurationSectionName}.{nameof(weatherApiConfiguration.BaseUrl)}");
                
                if (string.IsNullOrWhiteSpace(weatherApiConfiguration.ApiKey))
                    throw new ConfigurationException($"{WeatherApiConfiguration.ConfigurationSectionName}.{nameof(weatherApiConfiguration.ApiKey)}");

                httpClient.BaseAddress = new Uri(weatherApiConfiguration.BaseUrl);
            });

        serviceCollection
            .AddScoped<IWeatherApiClient, WeatherApiClient>();
        
        return serviceCollection;
    }
}
