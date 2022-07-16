using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.Configuration;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.Configuration;
using TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.DTO;

namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;

public class WeatherApiClient : IWeatherApiClient
{
    public const string HttpClientName = "WeatherApi";

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly WeatherApiConfiguration _weatherApiConfiguration;

    private readonly IRequestUriBuilder _requestUriBuilder;



    public WeatherApiClient(IOptionsSnapshot<WeatherApiConfiguration> configurationSnapshot, IHttpClientFactory httpClientFactory, IRequestUriBuilder requestUriBuilder)
    {
        _weatherApiConfiguration = configurationSnapshot.Value ?? throw new ConfigurationException(WeatherApiConfiguration.ConfigurationSectionName);

        if (string.IsNullOrWhiteSpace(_weatherApiConfiguration.ApiKey))
            throw new ConfigurationException($"{WeatherApiConfiguration.ConfigurationSectionName}{nameof(_weatherApiConfiguration.ApiKey)}");

        _httpClientFactory = httpClientFactory;

        _requestUriBuilder = requestUriBuilder;
    }

    
    
    public async Task<Forecast> GetForecast(decimal latitude, decimal longitude, int numberOfDays)
    {
        using var httpClient = _httpClientFactory.CreateClient(HttpClientName);
        
        var response = await httpClient.GetAsync(_requestUriBuilder.BuildForecastingUri(_weatherApiConfiguration.ApiKey!, latitude, longitude, numberOfDays));
        response.EnsureSuccessStatusCode();
        
        var typedResponse = await response.Content.ReadFromJsonAsync<ForecastResponse>();

        return typedResponse!.Forecast;
    }
}
