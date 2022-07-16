using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Configuration;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.HttpClient;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.Configuration;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.TuiMusementApi.DI;

public static class TuiMusementApiServiceCollectionExtensions
{
    public static IServiceCollection AddTuiMusementHttpApi(this IServiceCollection serviceCollection, IConfigurationRoot configuration)
    {
        serviceCollection
            .Configure<TuiApiConfiguration>(configuration.GetSection(TuiApiConfiguration.ConfigurationSectionName));

        serviceCollection
            .AddHttpClient(TuiMusementApiClient.HttpClientName, client =>
            {
                var tuiApiConfiguration = configuration.GetSection(TuiApiConfiguration.ConfigurationSectionName).Get<TuiApiConfiguration>();
                
                if (string.IsNullOrWhiteSpace(tuiApiConfiguration.BaseUrl))
                    throw new ConfigurationException($"{TuiApiConfiguration.ConfigurationSectionName}.{nameof(tuiApiConfiguration.BaseUrl)}");
                
                client.BaseAddress = new Uri(tuiApiConfiguration.BaseUrl);
                client.DefaultRequestHeaders.AddAcceptApplicationJson();
            })
            .AddPolicyHandler(ExponentialBackoffRetryPolicyBuilder.GetPolicy(5));

        serviceCollection
            .AddScoped<ITuiMusementApiClient, TuiMusementApiClient>();

        return serviceCollection;
    }
}
