namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;

public interface IRequestUriBuilder
{
    public string BuildForecastingUri(string apiKey, decimal latitude, decimal longitude, int numberOfDays);
}
