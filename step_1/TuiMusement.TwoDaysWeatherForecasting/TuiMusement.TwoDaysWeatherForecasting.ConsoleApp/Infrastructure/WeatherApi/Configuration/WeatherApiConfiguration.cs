namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi.Configuration;

public class WeatherApiConfiguration
{
    public const string ConfigurationSectionName = "WeatherApi";
    
    public string? BaseUrl { get; set; }
    
    public string? ApiKey { get; set; }
}
