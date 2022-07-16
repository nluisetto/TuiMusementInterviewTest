namespace TuiMusement.TwoDaysWeatherForecasting.ConsoleApp.Infrastructure.WeatherApi;

public class RequestUriBuilder : IRequestUriBuilder
{
    public string BuildForecastingUri(string apiKey, decimal latitude, decimal longitude, int numberOfDays) =>
        $"{Paths.Forecast}?key={apiKey}&q={latitude},{longitude}&days={numberOfDays}";
}
